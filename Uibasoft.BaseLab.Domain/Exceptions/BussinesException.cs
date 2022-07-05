using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Exceptions
{
    public class BussinesException : Exception
    {
        public BussinesException()
        {

        }

        public BussinesException(string message) : base(message)
        {

        }
    }
}
