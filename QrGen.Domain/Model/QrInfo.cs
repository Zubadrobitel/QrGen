namespace QrGen.Domain.Model
{
    public sealed class QrInfo
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int GuestCount { get; set; }

        public QrInfo() { }
    }
}
