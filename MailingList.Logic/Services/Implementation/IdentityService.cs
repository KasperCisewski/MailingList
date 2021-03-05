using MailingList.Data.Domains;
using MailingList.Data.Repository.Abstraction;
using MailingList.Logic.Models.Responses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MailingList.Logic.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;

        public IdentityService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthorizationSuccessResponse GenerateAuthorizationResultForUser(User user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthorizationSuccessResponse
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

        ////unit
        public bool UserWithEmailExists(string email)
        {
            return _userRepository.GetAll().Any(u => u.Email.ToLower() == email.ToLower());
        }

        public bool UserWithUsernameExists(string username)
        {
            return _userRepository.GetAll().Any(u => u.UserName.ToLower() == username.ToLower());
        }
    }
}
