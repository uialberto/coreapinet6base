using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.Entities.Core;

namespace Uibasoft.BaseLab.Domain.Interfaces.Services
{
    public interface IServiceParametros
    {
        Task<Parametro> GetFirstParametrosync();
    }
}
