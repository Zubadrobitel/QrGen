using QrGen.DataBase.Entities;
using QrGen.Domain.Model;

namespace QrGen.DataBase.Mappers
{
    public static class QrInfoMapper
    {
        public static QrInfo ToDomain(this QrInfoEntity entity)
        {
            var domain = new QrInfo()
            {
                Id = entity.Id,
                Password = entity.Password,
                Start =entity.Start,
                End = entity.End,
                GuestCount = entity.GuestCount
            };

            if(domain == null)
                throw new Exception("Ошибка при создании QrInfo");

            return domain;
        }

        public static QrInfoEntity ToEntity(this QrInfo domain)
        {
            var entity = new QrInfoEntity
            {
               Id = domain.Id,
               Password = domain.Password,
               Start = domain.Start,
               End = domain.End,
               GuestCount = domain.GuestCount
            };

            return entity;
        }
    }
}
