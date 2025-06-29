using Application.Services;
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

        [HttpPost]
        [Route("CreateQrCode")]
        public async Task<ActionResult<Guid>> CreateQrCodeAsync([FromBody] QrCodeRequest request)
        {
            var qrCreateResult = QrInfo.Create(
                Guid.NewGuid(),
                request.password,
                request.start,
                request.end,
                request.guestCount
                );

            if (qrCreateResult.IsFailure)
                return BadRequest(string.Join(", ", qrCreateResult.Errors));
            if (qrCreateResult.Value == null)
                return BadRequest("Ошибка создания Qr кода");

            var result = await _service.GenerateQrCodeAsync(qrCreateResult.Value);

            return Ok(result);
        }

        //[HttpPost]
        //[Route("UpdateQrCode")]
        //public async Task<Guid> UpdateQrCodeAsync([FromBody] Guid id, [FromBody] QrCodeRequest request)
        //{
        //    var result = await _service.UpdateQrCodeAsync(id, request);
        //}

        [HttpGet]
        [Route("GetAllQrCodes")]
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

        [HttpPost]
        [Route("DeleteQrCode")]
        public async Task DeleteQrByIdAsync(Guid id) => await _service.DeleteQrCodeByIdAsync(id);

        [HttpGet("{id}")]
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


