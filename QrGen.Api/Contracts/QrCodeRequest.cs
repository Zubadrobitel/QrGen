using System.ComponentModel.DataAnnotations;

namespace QrGen.Api.Contracts
{
    public record QrCodeRequest(
        [Required] string password,
        [Required] DateTime start,
        [Required] DateTime end,
        [Required] int guestCount
        );
}
