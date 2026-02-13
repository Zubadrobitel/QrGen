using Microsoft.EntityFrameworkCore;
using QrGen.DataBase.Entities;
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

        public async Task<Guid> AddAsync(QrCode qr)
        {
            var entity = qr.ToEntity();
            await _context.QrCodes.AddAsync(entity); 
            var result = await _context.SaveChangesAsync();
            return entity.Id;
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

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.Qrinfos
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();

            return id;
        }


        public async Task<Guid> UpdateQrCodeASync(QrInfo request)
        {
            if (request is null)
                throw new Exception("Пустой запрос!");

            QrInfoEntity data = new()
            {
                Id = request.Id,
                GuestCount = request.GuestCount,
                Start = request.Start,
                End = request.End,
                Password = request.Password
            };

            try
            {
                _context.Qrinfos.Update(data);
                await _context.SaveChangesAsync();
                return data.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
