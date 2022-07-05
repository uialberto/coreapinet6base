using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.CustomEntities
{
    public class BaseErrorMessage
    {
        public string Message { get; set; }
        public string MoreInfo { get; set; }
        public int Type { get; set; }
        public string Key { get; set; }
    }
}
