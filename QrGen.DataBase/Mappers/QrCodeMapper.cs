using QrGen.DataBase.Entities;
using QrGen.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrGen.DataBase.Mappers
{
    public static class QrCodeMapper
    {
        public static QrCode ToDomain(this QrCodeEntity entity)
        {
            var domain = QrCode.Create(entity.Id,
                entity.CreatedAt,
                entity.UpdatedAt,
                entity.QrInfo.ToDomain()).Value;

            if (domain == null)
                throw new Exception("Ошибка при создании QrCode");

            return domain;
        }

        public static QrCodeEntity ToEntity(this QrCode domain)
        {
            var entity = new QrCodeEntity { 
                Id = domain.Id,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
                QrInfo = domain.Info.ToEntity()
            };

            return entity;
        }
    }
}
