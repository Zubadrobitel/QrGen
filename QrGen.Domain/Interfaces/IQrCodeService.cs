using QrGen.Domain.Model;
using QrGen.Domain.Model.DTO;

namespace QrGen.Domain.Interfaces
{
    public interface IQrCodeService
    {
        Task<Guid> DeleteQrCodeByIdAsync(Guid id);
        Task<Guid> GenerateQrCodeAsync(QrInfo? qrInfo);
        Task<QrResult> GetQrByIdAsync(Guid id);
        Task<List<QrResult>> GetAllQrCodesAsync();
        Task<Guid> UpdateQrCodeAsync(QrInfo? request);
    }
}