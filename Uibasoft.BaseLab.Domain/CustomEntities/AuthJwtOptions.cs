using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.CustomEntities
{
    public class AuthJwtOptions
    {
        public string Secret { get; set; }
        public int ExpiresMin { get; set; }
    }
}
