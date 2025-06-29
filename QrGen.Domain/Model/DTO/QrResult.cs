using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrGen.Domain.Model.DTO
{
    public class QrResult
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string QrCodeAsBase64 { get; set; } = string.Empty;
    }
}
