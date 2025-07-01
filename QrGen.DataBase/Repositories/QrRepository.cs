using Microsoft.EntityFrameworkCore;
using QrGen.DataBase.Mappers;
using QrGen.Domain.Interfaces;
using QrGen.Domain.Model;


namespace QrGen.DataBase.Repositories
{
    public sealed class QrRepository : IQrRepository
    {
        private readonly ApplicationDataBaseContext _context;

        public QrRepository(ApplicationDataBaseContext context)
        {
            _context = context;
        }

        public async Task<QrCode?> GetByIdAsync(Guid id)
        {
            var entity = await _context.QrCodes
                .AsNoTracking()
                .Include(q => q.QrInfo)
                .FirstOrDefaultAsync(q => q.Id == id);

            return entity?.ToDomain();
        }

        public async Task AddAsync(QrCode qr)
        {
            var entity = qr.ToEntity();
            await _context.QrCodes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<QrCode>> GetAllQrCodesAsync()
        {
            var qrcodeEntity = await _context.QrCodes
                .AsNoTracking()
                .Include(q => q.QrInfo)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            var qrcodes = qrcodeEntity
                .Select(x => QrCodeMapper.ToDomain(x))
                .ToList();

            return qrcodes;
        }

        public async Task DeleteAsync(Guid id) =>
            await _context.Qrinfos
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();
    }
}
