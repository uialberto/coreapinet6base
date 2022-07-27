using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.DataAccess.Mapping.Core;
using Uibasoft.BaseLab.Domain.Entities;
using Uibasoft.BaseLab.Domain.Entities.Core;

namespace Uibasoft.BaseLab.DataAccess.UnitOfWorks
{
    public partial class AppCoreContext : DbContext, IAppCoreContext
    {
        public AppCoreContext()
        {
        }

        public AppCoreContext(DbContextOptions<AppCoreContext> options) : base(options)
        {
        }

        public virtual DbSet<Parametro> Parametros { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ParametroConfig());
        }

    }
}
