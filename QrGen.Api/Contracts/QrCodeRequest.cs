using System.ComponentModel.DataAnnotations;

namespace QrGen.Api.Contracts
{
    public record QrCodeRequest(
        Guid Id,
        string Password,
        DateTime Start,
        DateTime End,
        int GuestCount
        );
}