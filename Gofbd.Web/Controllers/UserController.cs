namespace Gofbd.Web.Controllers
{
    using Gofbd.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;
        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //Get user details
            this.logger.LogInformation("Usercontrollers:Get called.");

            var email = HttpContext.User.Identity.Name;
            this.logger.LogInformation($"Authenticated user {email}");
            return Ok(email);
        }
    }
}