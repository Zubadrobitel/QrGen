using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrGen.DataBase.Entities;

namespace QrGen.DataBase.Configurations
{
    public class QrCodeEntityConfiguration : IEntityTypeConfiguration<QrCodeEntity>
    {
        public void Configure(EntityTypeBuilder<QrCodeEntity> builder)
        {
            builder.HasKey(qc => qc.Id);
        }
    }
}