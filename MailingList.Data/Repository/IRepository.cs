using MailingList.Data.Domains.Base;
using System.Linq;
using System.Threading.Tasks;

namespace MailingList.Data.Repository
{
    public interface IRepository<T, TK> where T : IEntity<TK>
    {
        IQueryable<IEntity<TK>> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Remove(IEntity<TK> entity);
        Task RemoveById(TK id);
        Task<IEntity<TK>> GetById(TK id);
    }
}
