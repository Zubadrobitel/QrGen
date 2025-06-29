using QrGen.Domain.Model;
using QrGen.Domain.Model.DTO;

namespace QrGen.Domain.Interfaces
{
    public interface IQrCodeService
    {
        Task DeleteQrCodeByIdAsync(Guid id);
        Task<Guid> GenerateQrCodeAsync(QrInfo qrInfo);
        Task<Guid> UpdateQrCodeAsync(QrCode qrCode);
        Task<QrResult> GetQrByIdAsync(Guid id);
        Task<List<QrResult>> GetAllQrCodesAsync();
    }
}