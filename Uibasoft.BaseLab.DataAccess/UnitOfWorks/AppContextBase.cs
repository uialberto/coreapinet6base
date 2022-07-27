using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.Interfaces;

namespace Uibasoft.BaseLab.DataAccess.UnitOfWorks
{
    public class AppContextBase<TEntity> : IAppContextBase<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _entities;
        internal AppCoreContext AppContext { get { return _context; } }

        private readonly AppCoreContext _context;
        public AppContextBase(AppCoreContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public int Count()
        {
            return _entities.Count();
        }
        public async Task<int> SaveAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            var query = _entities;
            return query;
        }

        public async Task<TEntity> SingleOrDefaultAsync()
        {
            return await _entities.SingleOrDefaultAsync();
        }

        public void UpdateEntity(TEntity entity)
        {
            _entities.Update(entity);
        }
    }
}
