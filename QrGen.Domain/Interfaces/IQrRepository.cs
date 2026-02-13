using QrGen.Domain.Model;

namespace QrGen.Domain.Interfaces
{
    public interface IQrRepository
    {
        Task<Guid> AddAsync(QrCode qr);
        Task<List<QrCode>> GetAllQrCodesAsync();
        Task<Guid> DeleteAsync(Guid id);
        Task<QrCode?> GetByIdAsync(Guid id);
        Task<Guid> UpdateQrCodeASync(QrInfo request);
    }
}