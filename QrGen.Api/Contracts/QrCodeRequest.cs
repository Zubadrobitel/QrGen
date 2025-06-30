using System.ComponentModel.DataAnnotations;

namespace QrGen.Api.Contracts
{
    public record QrCodeRequest(
        [Required] string Password,
        [Required] DateTime Start,
        [Required] DateTime End,
        [Required] int GuestCount
        );
}
