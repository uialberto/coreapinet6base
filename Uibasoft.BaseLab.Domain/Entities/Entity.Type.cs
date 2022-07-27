using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.Interfaces;

namespace Uibasoft.BaseLab.Domain.Entities
{
    public abstract class Entity<Type> : IEntity<Type>
    {
        public Type Id { get; set; }
    }
}
