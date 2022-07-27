using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.Entities.Core;

namespace Uibasoft.BaseLab.DataAccess.UnitOfWorks
{
    public interface IAppCoreContext
    {
        DbSet<Parametro> Parametros { get; set; }
    }
}
