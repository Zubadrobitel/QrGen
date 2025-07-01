using QrGen.Domain.Model;

namespace QrGen.Domain.Interfaces
{
    public interface IQrRepository
    {
        Task AddAsync(QrCode qr);
        Task<List<QrCode>> GetAllQrCodesAsync();
        Task DeleteAsync(Guid id);
        Task<QrCode?> GetByIdAsync(Guid id);
    }
}