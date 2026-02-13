using QrGen.DataBase.Entities;
using QrGen.Domain.Model;

namespace QrGen.DataBase.Mappers
{
    public static class QrCodeMapper
    {
        public static QrCode ToDomain(this QrCodeEntity entity)
        {
            var domain = new QrCode()
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Info = entity.QrInfo.ToDomain()
            };

            return domain;
        }

        public static QrCodeEntity ToEntity(this QrCode domain)
        {
            var entity = new QrCodeEntity
            {
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
                QrInfo = new QrInfoEntity()
                {
                    Password = domain.Info.Password,
                    Start = domain.Info.Start,
                    End = domain.Info.End,
                    GuestCount = domain.Info.GuestCount
                }
            };

            return entity;
        }
    }
}
