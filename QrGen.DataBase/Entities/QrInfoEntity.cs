namespace QrGen.DataBase.Entities
{
    public sealed class QrInfoEntity
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int GuestCount { get; set; }

        public QrCodeEntity QrCode { get; set; }
    }
}
