using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace base_template.Controllers
{
    [ApiController]
    public class ApiTestController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return Content("Running...");
        }

        [EnableCors("CorsPolicy")]
        [Route("api/test/isAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            return new ObjectResult(User.Identity.IsAuthenticated);
        }

        [Route("api/test/fail")]
        public IActionResult Fail()
        {
            return Unauthorized();
        }

        [Route("api/test/name")]
        [Authorize]
        public IActionResult Name()
        {
            var claimsPrincipal = (ClaimsPrincipal)User;
            var givenName = claimsPrincipal.FindFirst(ClaimTypes.GivenName).Value;
            return Ok(givenName);
        }

        [Route("api/test/[action]")]
        public IActionResult Denied()
        {
            return Content("You need to allow this application access in Google order to be able to login");
        }

    }
}