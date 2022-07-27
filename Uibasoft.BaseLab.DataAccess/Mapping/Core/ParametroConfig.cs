using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uibasoft.BaseLab.Domain.Entities.Core;

namespace Uibasoft.BaseLab.DataAccess.Mapping.Core
{
    public class ParametroConfig : IEntityTypeConfiguration<Parametro>
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            builder.Property(e => e.Empresa).HasColumnName("Empresa");
            builder.Property(e => e.Empresa).HasMaxLength(150);
            builder.Property(e => e.Empresa).IsUnicode(false);
            builder.Property(e => e.Empresa).IsRequired(false);

            builder.Property(e => e.CreateDateUtc).IsRequired();
            builder.Property(e => e.CreateDateUtc).HasColumnName("CreateDateUtc");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Id).HasColumnName("IdParametro");
            builder.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("newsequentialid()");

            builder.ToTable("Parametros", "core");

        }
    }
}
