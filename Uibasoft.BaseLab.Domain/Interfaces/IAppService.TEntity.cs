using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Interfaces
{
    public interface IAppService<TEntity>
    {
        Guid InstanceId { get; }
        Task<TEntity> SingleOrDefaultAsync();
        Task AddAsync(TEntity entity);
        Task<int> SaveAsync(TEntity entity);

        void UpdateEntity(TEntity entity);
        IQueryable<TEntity> GetAll();
        int Count();
    }
}
