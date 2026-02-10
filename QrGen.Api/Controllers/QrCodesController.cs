using Microsoft.AspNetCore.Mvc;
using QrGen.Api.Contracts;
using QrGen.Domain.Model;
using QrGen.Domain.Interfaces;

namespace QrGen.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QrCodesController : ControllerBase
    {
        private readonly IQrCodeService _service;
        public QrCodesController(IQrCodeService service)
        {
            _service = service;
        }

        [HttpPost("create/")]
        public async Task<ActionResult<Guid>> CreateQrCodeAsync([FromBody] QrCodeRequest request)
        {
            var qrCreateResult = QrInfo.Create(
                Guid.NewGuid(),
                request.Password,
                request.Start,
                request.End,
                request.GuestCount
                );

            if (qrCreateResult.IsFailure)
                return BadRequest(string.Join(", ", qrCreateResult.Errors));
            if (qrCreateResult.Value == null)
                return BadRequest("Ошибка создания Qr кода");

            var result = await _service.GenerateQrCodeAsync(qrCreateResult.Value);
            if(result.IsFailure)
                return NotFound(string.Join(", ", result.Errors));

            return Ok(result.Value);
        }

        [HttpGet("qr-codes/")]
        public async Task<ActionResult<List<QrCodeResponse>>> GetAllQrCodesAsync()
        {
            var result = await _service.GetAllQrCodesAsync();
            if(result.IsFailure)
                return NotFound(string.Join(", ", result.Errors));

            return Ok(result.Value);  
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteQrByIdAsync(Guid id) => await _service.DeleteQrCodeByIdAsync(id);

        [HttpGet("qr-code/{id}")]
        public async Task<ActionResult<QrCodeResponse>> GetQrById(Guid id)
        {
            var response = await _service.GetQrByIdAsync(id);
            if(response.IsFailure)
                return NotFound(string.Join(", ", response.Errors));

            return Ok(response.Value);
        }
    }
}


