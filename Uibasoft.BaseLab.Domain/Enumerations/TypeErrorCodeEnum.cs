using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Enumerations
{
    public enum TypeErrorCodeEnum
    {
        Success = 0,
        GenericError = 1,
        UserBlocked = 2,
        TokenExpired = 3,
        TokenNullOrEmpty = 4,
        WithoutAutorization = 5

    }
}
