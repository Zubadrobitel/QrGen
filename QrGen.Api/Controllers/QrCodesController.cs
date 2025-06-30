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

        [HttpPost("create-qr-code/")]
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

            return Ok(result);
        }

        [HttpGet("get-all-qr-codes/")]
        public async Task<ActionResult<List<QrCodeResponse>>> GetAllQrCodesAsync()
        {
            try
            {
                var result = await _service.GetAllQrCodesAsync();
                return Ok(result);  
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost("delete-qr-code/{id}")]
        public async Task DeleteQrByIdAsync(Guid id) => await _service.DeleteQrCodeByIdAsync(id);

        [HttpGet("get-qr-by-id/{id}")]
        public async Task<ActionResult<QrCodeResponse>> GetQrById(Guid id)
        {
            try
            {
                var response = await _service.GetQrByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}


