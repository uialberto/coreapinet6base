using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.CustomEntities
{
    public class AppSettingsConfigOptions
    {        
        public int TimeoutServiceMiliSeg { get; set; }
        public int DefaultPageIndex { get; set; }
        public int DefaultPageSize { get; set; }
    }
}
