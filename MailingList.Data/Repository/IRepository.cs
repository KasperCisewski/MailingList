﻿using MailingList.Data.Domains.Base;
using System.Linq;
using System.Threading.Tasks;

namespace MailingList.Data.Repository
{
    public interface IRepository<T, TK> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        Task RemoveById(TK id);
        Task<T> GetById(TK id);
    }
}
