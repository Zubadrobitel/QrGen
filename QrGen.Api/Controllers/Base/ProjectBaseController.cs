using Microsoft.AspNetCore.Mvc;

namespace QrGen.Api.Controllers.Base
{
    public class ProjectBaseController() : ControllerBase
    {
        public async Task<IActionResult> GetAnswerAsync<T>(Func<Task<T>> action)
        {
			try
			{
				T result = await action();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
        }
    }
}