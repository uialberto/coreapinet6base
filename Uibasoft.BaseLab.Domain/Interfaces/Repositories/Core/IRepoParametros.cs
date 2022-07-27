using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.Entities.Core;

namespace Uibasoft.BaseLab.Domain.Interfaces.Repositories.Core
{
    public interface IRepoParametros : IRepository<Parametro>
    {
        Task<Parametro> GetFirstParametrosAsync();
        Task<Parametro> GetParametroAsync(Guid id);
        Task<Parametro> Crear(Parametro entity);
        Task<Parametro> Editar(Parametro entity);
        Task<int> EliminarAsync(Guid id);

        Task<IEnumerable<Parametro>> ListAllAsync();
    }
}
