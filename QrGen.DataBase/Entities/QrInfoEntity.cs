using System.ComponentModel.DataAnnotations.Schema;

namespace QrGen.DataBase.Entities
{
    public sealed class QrInfoEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int GuestCount { get; set; }

        public QrCodeEntity QrCode { get; set; }

        public QrInfoEntity() { }
    }
}
