using MailingList.Data.Domains.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MailingList.Data.Repository
{
    public abstract class CrudRepository<T, TK> : IRepository<T, TK> where T : IEntity<TK>
    {
        protected readonly MailingListDbContext Context;
        private readonly DbSet<IEntity<TK>> _table;

        protected CrudRepository(MailingListDbContext context)
        {
            Context = context;
            _table = Context.Set<IEntity<TK>>();
        }
        public async Task<T> Add(T entity)
        {
            await _table.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public IQueryable<IEntity<TK>> GetAll()
        {
            return _table.AsQueryable();
        }

        public async Task<IEntity<TK>> GetById(TK id)
        {
            var model = await _table.FindAsync(id);

            foreach (var reference in Context.Entry(model).References)
                await reference.LoadAsync();

            return model;
        }

        public async Task Remove(IEntity<TK> entity)
        {
            _table.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveById(TK id)
        {
            var entity = await GetById(id);
            await Remove(entity);
        }

        public async Task Update(T entity)
        {
            _table.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
