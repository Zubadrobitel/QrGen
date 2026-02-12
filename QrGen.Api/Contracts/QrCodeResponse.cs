using Swashbuckle.AspNetCore.Annotations;

namespace QrGen.Api.Contracts
{
    public record QrCodeResponse(
        [SwaggerSchema("Идентификатор Qr")]
        Guid Id,

        [SwaggerSchema("Дата создания Qr")]
        DateTime CreatedAt,

        [SwaggerSchema("Строка представления Qr в формате base64")]
        string QrCodeAsBase64
    );
}
