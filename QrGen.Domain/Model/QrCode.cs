using QrGen.Domain.Helpers;

namespace QrGen.Domain.Model
{
    public sealed class QrCode
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public QrInfo Info { get; }

        private QrCode(
            Guid id,
            DateTime createdAt,
            DateTime updatedAt,
            QrInfo qrInfo)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Info = qrInfo;
        }

        public static Result<QrCode> Create(
            Guid id,
            DateTime createdAt,
            DateTime updatedAt,
            QrInfo qrInfo)
        {

            var errors = new List<string>();

            if (createdAt > DateTime.UtcNow)
                errors.Add("Дата создания не должна быть больше текущей");
            if (qrInfo == null)
                errors.Add("Должна быть заполнена информация о QR");
            
            if(errors.Count > 0)
                return Result<QrCode>.Failure(errors);

            var qr = new QrCode(id, createdAt, updatedAt, qrInfo);
            return Result<QrCode>.Success(qr);
        }
    }

}
