namespace QrGen.DataBase.Entities
{
    public sealed class QrCodeEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid InfoId { get; set; }
        public QrInfoEntity QrInfo { get; set; }
    }
}