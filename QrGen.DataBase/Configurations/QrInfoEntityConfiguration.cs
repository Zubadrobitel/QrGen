using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QrGen.DataBase.Entities;

namespace QrGen.DataBase.Configurations
{
    public class QrInfoEntityConfiguration : IEntityTypeConfiguration<QrInfoEntity>
    {
        public void Configure(EntityTypeBuilder<QrInfoEntity> builder)
        {
            builder.HasKey(q => q.Id);

            builder.HasOne(q => q.QrCode)
                .WithOne(qc => qc.QrInfo)
                .HasForeignKey<QrCodeEntity>(qc => qc.InfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
