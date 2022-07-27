using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Guid InstanceId { get; }
        Task<int> SaveAsync(TEntity entity);
        Task<TEntity> SingleOrDefaultAsync();
        Task AddAsync(TEntity entity);

        void UpdateEntity(TEntity entity);
        IQueryable<TEntity> GetAll();
        int Count();
    }
}
