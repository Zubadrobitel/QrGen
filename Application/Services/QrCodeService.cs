using QrGen.Domain.Helpers;
using QrGen.Domain.Interfaces;
using QrGen.Domain.Model;
using QrGen.Domain.Model.DTO;

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

        public async Task<Result<List<QrResult>>> GetAllQrCodesAsync()
        {
            var qrList = await _repository.GetAllQrCodesAsync();

            if(qrList == null) 
                return Result<List<QrResult>>.Failure("Список пуст!");

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

            return Result<List<QrResult>>.Success(resultList);
        }

        public async Task<Result<Guid>> GenerateQrCodeAsync(QrInfo qrInfo)
        {
            var id = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;
            var updatedAt = DateTime.UtcNow;

            var qrModel = QrCode.Create(
                id,
                createdAt,
                updatedAt,
                qrInfo
                );

            if (qrModel.Value == null)
               return Result<Guid>.Failure("Ошибка создания(QR-код не может быть пустым!)");
            if(qrModel.IsFailure)
                return Result<Guid>.Failure(qrModel.Errors);

            await _repository.AddAsync(qrModel.Value);

            return Result<Guid>.Success(id);
        }

        public async Task<Result<QrResult>> GetQrByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result == null)
                return Result<QrResult>.Failure("QR-код с указанным ID не найден!");

            var QrContent = CreateQrContent(result.Info);
            var qrcodeAsBase64 = _generator.GenerateQrCodeAsBase64(QrContent);

            return Result<QrResult>.Success(new QrResult
            {
                Id = result.Info.Id,
                CreatedAt = result.CreatedAt,
                QrCodeAsBase64 = qrcodeAsBase64
            });
        }
        public async Task DeleteQrCodeByIdAsync(Guid id) => await _repository.DeleteAsync(id);

        private string CreateQrContent(QrInfo info) =>  
@$"Пароль: {info.Password},
Начало: {info.Start},
Конец: {info.End},
Количество гостей: {info.GuestCount}";

        public async Task<Guid> UpdateQrCodeAsync(QrInfo? request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));

            return await _repository.UpdateQrCodeASync(request);
        }
    }
}
