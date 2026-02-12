using Swashbuckle.AspNetCore.Annotations;

namespace QrGen.Api.Contracts
{
    public record QrCodeRequest(
        [SwaggerSchema("Идентификатор Qr")]
        Guid Id,

        [SwaggerSchema("Пароль вшиты в Qr")]
        string Password,

        [SwaggerSchema("Дата начала по Qr")]
        DateTime Start,

        [SwaggerSchema("Дата окончания по Qr")]
        DateTime End,

        [SwaggerSchema("Кол-во гостей")]
        int GuestCount
        );
}