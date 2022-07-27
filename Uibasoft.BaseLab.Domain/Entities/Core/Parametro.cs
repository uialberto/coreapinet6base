using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Entities.Core
{
    public partial class Parametro : Entity<Guid>
    {
        public string Empresa { get; set; }
        public DateTime CreateDateUtc { get; set; }
    }
}
