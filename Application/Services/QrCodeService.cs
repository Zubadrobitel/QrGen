using QrGen.Domain.Helpers;
using QrGen.Domain.Interfaces;
using QrGen.Domain.Model;
using QrGen.Domain.Model.DTO;
using QrGen.Domain.Model.MassTransit;

namespace Application.Services
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IQrRepository _repository;
        private readonly IQrCodeGenerator _generator;
        private readonly IEventPublisher _publisher;

        public QrCodeService(IQrRepository repository,
            IQrCodeGenerator generator,
            IEventPublisher publisher)
        {
            _repository = repository;
            _generator = generator;
            _publisher = publisher;
        }

        public async Task<List<QrResult>> GetAllQrCodesAsync()
        {
            var qrList = await _repository.GetAllQrCodesAsync();
            if(qrList == null) 
                throw new Exception("Список пуст!");

            var resultList = new List<QrResult>();

            foreach (var qrCode in qrList)
            {
                var QrContent = CreateQrContent(qrCode.Info);
                var qrcodeAsBase64 = _generator.GenerateQrCodeAsBase64(QrContent);

                resultList.Add(new QrResult
                {
                    Id = qrCode.Info.Id,
                    CreatedAt = qrCode.CreatedAt,
                    QrCodeAsBase64 = qrcodeAsBase64
                });
            }

            return resultList;
        }

        public async Task<Guid> GenerateQrCodeAsync(QrInfo? qrInfo)
        {
            ValidateData(qrInfo);

            QrCode qrModel = new()
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Info = qrInfo!
            };
            if (qrModel == null)
               throw new Exception("Ошибка создания(QR-код не может быть пустым!");

            qrModel.Id = await _repository.AddAsync(qrModel);
            await _publisher.PublishEventAsync(
            new BaseEvent<QrCode>
            {
                Data = qrModel,
                CreatedAt = DateTime.UtcNow
            });

            return qrModel.Id;
        }

        public async Task<QrResult> GetQrByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                throw new Exception("QR-код с указанным ID не найден!");

            var QrContent = CreateQrContent(result.Info);
            var qrcodeAsBase64 = _generator.GenerateQrCodeAsBase64(QrContent);

            await _publisher.PublishEventAsync(
            new BaseEvent<QrCode>
            {
                Data = result,
                CreatedAt = DateTime.UtcNow
            });

            return new QrResult()
            {
                Id = result.Info.Id,
                CreatedAt = result.CreatedAt,
                QrCodeAsBase64 = qrcodeAsBase64
            };
        }
        public async Task<Guid> DeleteQrCodeByIdAsync(Guid id) => await _repository.DeleteAsync(id);

        private string CreateQrContent(QrInfo info) =>  
@$"Пароль: {info.Password},
Начало: {info.Start},
Конец: {info.End},
Количество гостей: {info.GuestCount}";

        public async Task<Guid> UpdateQrCodeAsync(QrInfo? request)
        {
            ValidateData(request);

            Guid qrId = await _repository.UpdateQrCodeASync(request!);

            await _publisher.PublishEventAsync(
                new BaseEvent<QrInfo>
                { 
                    Data = request,
                    CreatedAt = DateTime.UtcNow
                });

            return qrId;
        }

        private void ValidateData(QrInfo? request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (request.GuestCount <= 0)
                throw new Exception("Количество гостей не может быть меньше 1!");
            if (request.End <= DateTime.Now)
                throw new Exception("Дата начала не может быть в прошлом!");
        }
    }
}
