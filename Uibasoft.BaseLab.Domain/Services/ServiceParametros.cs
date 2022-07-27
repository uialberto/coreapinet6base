using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.Entities.Core;
using Uibasoft.BaseLab.Domain.Interfaces.Repositories.Core;
using Uibasoft.BaseLab.Domain.Interfaces.Services;

namespace Uibasoft.BaseLab.Domain.Services
{
    public class ServiceParametros : IServiceParametros
    {
        private readonly IRepoParametros _repo;
        public ServiceParametros(IRepoParametros pRepo)
        {
            _repo = pRepo;
        }

        public async Task<Parametro> GetFirstParametrosync()
        {
            return await _repo.GetFirstParametrosAsync();
        }

    }
}
