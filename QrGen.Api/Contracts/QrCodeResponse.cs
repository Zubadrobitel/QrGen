namespace QrGen.Api.Contracts
{
    public record QrCodeResponse(
        Guid Id,
        DateTime CreatedAt,
        string QrCodeAsBase64
    );
}
