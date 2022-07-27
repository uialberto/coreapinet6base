using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.Interfaces;

namespace Uibasoft.BaseLab.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Guid _instanceId;
        private readonly IAppContextBase<TEntity> _dbContext;

        public Guid InstanceId => _instanceId;
        public Repository(IAppContextBase<TEntity> dbContext)
        {
            _instanceId = Guid.NewGuid();
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<TEntity> SingleOrDefaultAsync()
        {
            return await _dbContext.SingleOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.GetAll();
        }
        public void UpdateEntity(TEntity entity)
        {
            _dbContext.UpdateEntity(entity);
        }
        public int Count()
        {
            return _dbContext.Count();
        }

        public async Task<int> SaveAsync(TEntity entity)
        {
            return await _dbContext.SaveAsync(entity);
        }
    }
}
