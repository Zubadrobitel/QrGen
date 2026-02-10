using QrGen.Domain.Helpers;
using QrGen.Domain.Model;
using QrGen.Domain.Model.DTO;

namespace QrGen.Domain.Interfaces
{
    public interface IQrCodeService
    {
        Task DeleteQrCodeByIdAsync(Guid id);
        Task<Result<Guid>> GenerateQrCodeAsync(QrInfo qrInfo);
        Task<Result<QrResult>> GetQrByIdAsync(Guid id);
        Task<Result<List<QrResult>>> GetAllQrCodesAsync();
        Task<Guid> UpdateQrCodeAsync(QrInfo? request);
    }
}