namespace QrGen.Domain.Model
{
    public sealed class QrCode
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public QrInfo Info { get; set; }

        public QrCode() {}
    }
}
