using Microsoft.AspNetCore.Mvc;
using QrGen.Api.Contracts;
using QrGen.Domain.Model;
using QrGen.Domain.Interfaces;
using QrGen.Api.Controllers.Base;

namespace QrGen.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QrCodesController : ProjectBaseController
    {
        private readonly IQrCodeService _service;
        public QrCodesController(IQrCodeService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateQrCodeAsync([FromBody] QrInfo request)
            => await GetAnswerAsync(async () => await _service.GenerateQrCodeAsync(request));

        [HttpGet("QrCodes")]
        public async Task<IActionResult> GetAllQrCodesAsync()
            => await GetAnswerAsync(async () => await _service.GetAllQrCodesAsync());

        [HttpDelete("delete/{id}")]
        public async Task DeleteQrByIdAsync(Guid id) => await _service.DeleteQrCodeByIdAsync(id);

        [HttpGet("QrCode/{id}")]
        public async Task<IActionResult> GetQrById(Guid id)
            => await GetAnswerAsync(async () => await _service.GetQrByIdAsync(id));

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateQrInfo([FromBody] QrInfo request)
            => await GetAnswerAsync(async () => await _service.UpdateQrCodeAsync(request));
    }
}


