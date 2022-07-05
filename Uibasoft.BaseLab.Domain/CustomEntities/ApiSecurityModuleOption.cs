using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.CustomEntities
{
    public class ApiSecurityModuleOption
    {
        public ApiSecurityModule[] UsersModule { get; set; }
    }

    public class ApiSecurityModule
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
