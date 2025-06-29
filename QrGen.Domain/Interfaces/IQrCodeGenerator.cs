namespace QrGen.Domain.Interfaces
{
    public interface IQrCodeGenerator
    {
        string GenerateQrCodeAsBase64(string content);
    }
}