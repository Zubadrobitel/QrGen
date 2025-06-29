using QrGen.DataBase.Entities;
using QrGen.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrGen.DataBase.Mappers
{
    public static class QrInfoMapper
    {
        public static QrInfo ToDomain(this QrInfoEntity entity)
        {
            var domain = QrInfo.Create(
                entity.Id,
                entity.Password,
                entity.Start,
                entity.End,
                entity.GuestCount).Value;

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
