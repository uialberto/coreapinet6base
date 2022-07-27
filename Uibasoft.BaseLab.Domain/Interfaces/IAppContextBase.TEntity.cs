using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Interfaces
{
    public interface IAppContextBase<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task<int> SaveAsync(TEntity entity);
        Task<TEntity> SingleOrDefaultAsync();
        IQueryable<TEntity> GetAll();
        int Count();
        void UpdateEntity(TEntity entity);
    }
}
