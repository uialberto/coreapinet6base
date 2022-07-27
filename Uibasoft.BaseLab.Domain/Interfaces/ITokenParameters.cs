using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Interfaces
{
    public interface ITokenParameters
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Id { get; set; }
        public List<KeyValuePair<string, string>> KeyRoles { get; set; }

    }
}
