using QrGen.Domain.Helpers;
using QrGen.Domain.Interfaces;
using QrGen.Domain.Model;
using QrGen.Domain.Model.DTO;
using System.Text.Json;

namespace Application.Services
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IQrRepository _repository;
        private readonly IQrCodeGenerator _generator;

        public QrCodeService(IQrRepository repository,
            IQrCodeGenerator generator)
        {
            _repository = repository;
            _generator = generator;
        }

        public async Task<List<QrResult>> GetAllQrCodesAsync()
        { 
            var qrList = await _repository.GetAllQrCodesAsync();
            if (qrList == null)
                throw new Exception("Список пуст!");

            var resultList = new List<QrResult>();

            foreach (var qrCode in qrList)
            {
                var QrContent = CreateQrContent(qrCode.Info); //Можно было просто сделать выгрузку всех info из бд
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

        public async Task<Guid> GenerateQrCodeAsync(QrInfo qrInfo)
        {
            var id = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;
            var updatedAt = DateTime.UtcNow;

            var qrModel = QrCode.Create(id, createdAt, updatedAt, qrInfo);

            if (qrModel.Value == null)
                throw new Exception("QR-код не может быть пустым!");

            await _repository.AddAsync(qrModel.Value);

            return id;
        }
        public async Task<Guid> UpdateQrCodeAsync(QrCode qrCode)
        {
                var result = await _repository.GetByIdAsync(qrCode.Id)
            ?? throw new Exception("QR-код с указанным ID не найден!");

            var id = qrCode.Id;
            var updatedAt = DateTime.UtcNow;

            var qrModel = QrCode.Create(id, qrCode.CreatedAt, updatedAt, qrCode.Info);

            if (qrModel.Value == null)
                throw new Exception("QR-код не может быть пустым!");

            await _repository.UpdateAsync(qrModel.Value);

            return id;
        }

        public async Task<QrResult> GetQrByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id)
                ?? throw new Exception("QR-код с указанным ID не найден!");

            var QrContent = CreateQrContent(result.Info);
            var qrcodeAsBase64 = _generator.GenerateQrCodeAsBase64(QrContent);

            return new QrResult
            {
                Id = result.Info.Id,
                CreatedAt = result.CreatedAt,
                QrCodeAsBase64 = qrcodeAsBase64
            };
        }
        public async Task DeleteQrCodeByIdAsync(Guid id) => await _repository.DeleteAsync(id);

        private string CreateQrContent(QrInfo info) =>  
@$"Пароль: {info.Password},
Начало: {info.Start},
Конец: {info.End},
Количество гостей: {info.GuestCount}";

    }
}
