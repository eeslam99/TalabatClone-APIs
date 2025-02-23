using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;

namespace Route.Talabat.APIs.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : ControllerBase
	{
		public ActionResult Error(int code)
		{
			if (code == 400)
				return BadRequest(new ApiResponse(400));
			else if (code == 401)
				return NotFound(new ApiResponse(401));
			else if (code == 404)
				return Unauthorized(new ApiResponse(code));
			else
				return StatusCode(code);
		}
	}
}
