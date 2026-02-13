using System.ComponentModel.DataAnnotations.Schema;

namespace QrGen.DataBase.Entities
{
    public sealed class QrCodeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid InfoId { get; set; }
        public QrInfoEntity QrInfo { get; set; }
    }
}