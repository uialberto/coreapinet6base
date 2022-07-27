using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.DataAccess.UnitOfWorks;
using Uibasoft.BaseLab.Domain.Entities.Core;
using Uibasoft.BaseLab.Domain.Exceptions;
using Uibasoft.BaseLab.Domain.Interfaces;
using Uibasoft.BaseLab.Domain.Interfaces.Repositories.Core;

namespace Uibasoft.BaseLab.DataAccess.Repositories.Core
{
    public class RepoParametros : Repository<Parametro>, IRepoParametros
    {
        internal AppCoreContext DbContext { get; }
        public RepoParametros(IAppContextBase<Parametro> context) : base(context)
        {
            DbContext = (context as AppContextBase<Parametro>).AppContext ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Parametro> Crear(Parametro entity)
        {
            await SaveAsync(entity);
            return entity;
        }

        public async Task<Parametro> Editar(Parametro entityDto)
        {
            var entity = await DbContext.Parametros.FindAsync(entityDto.Id);
            if (entity == null)
                return entity;
            entity.Empresa = entityDto.Empresa;
            entity.CreateDateUtc = entityDto.CreateDateUtc;
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Parametro> GetFirstParametrosAsync()
        {
            return await DbContext.Parametros.FirstOrDefaultAsync();
        }

        public async Task<Parametro> GetParametroAsync(Guid id)
        {
            return await DbContext.Parametros.Where(ele => ele.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Parametro>> ListAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<int> EliminarAsync(Guid id)
        {
            var entity = await DbContext.Parametros.FindAsync(id);
            if (entity == null) return -1;

            //Regla Negocio 1
            if ((DateTime.UtcNow - entity.CreateDateUtc).TotalDays <= 5)
            {
                throw new BussinesException("No se permite eliminar Parametro por Regla Negocio 001NA");
            }

            DbContext.Parametros.Remove(entity);
            var result = await DbContext.SaveChangesAsync();
            return result;
        }
    }
}
