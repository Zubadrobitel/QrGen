using QRCoder;
using QrGen.Domain.Interfaces;

namespace QrCodeGenerator.Service
{
    public class QrCodeGenerator : IQrCodeGenerator
    {
        public string GenerateQrCodeAsBase64(string content)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new Base64QRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }
    }
}