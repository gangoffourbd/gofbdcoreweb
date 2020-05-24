namespace Gofbd.Web.Controllers
{
    using Gofbd.Core.Infrastructure.ExtensionMetthod;
    using Gofbd.Dto;
    using Gofbd.Feature;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net.Http.Headers;
    using System.Text;

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediatR;
        private const string SplitChar = "~|~";

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IConfiguration configuration,
            IMediator mediator)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._mediatR = mediator;
        }

        [HttpPost]
        public IActionResult SignIn()
        {
            var headerKey = this._configuration["Authentication:HeaderKey"];
            if (!Request.Headers.ContainsKey(headerKey))
            {
                this._logger.LogInformation("Must sent authentication information through header. Authoriztion value was not found in header.");
                return NotFound("Authorization header was not found.");
            }

            var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers[headerKey]);
            var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
            var plain = Encoding.Unicode.GetString(bytes);

            if (!plain.IsValidCredentialFormat())
                return BadRequest("Authorization header must contain two strings.");

            var splitted = plain.Split(SplitChar, StringSplitOptions.RemoveEmptyEntries);
            this._logger.LogInformation(plain);

            var userId = splitted[0].Trim('~');
            var password = splitted[1].Trim('~');
            var signIncommand = new SignInCommand()
            {
                UserName = userId,
                Password = password
            };
            var user = this._mediatR.Send(signIncommand);

            if (user == null)
                return BadRequest(new { message = "Incorrect Username or Password." });
            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SignOut()
        {
            //TODO: Implement signout
            return Ok();
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterUserCommand command)
        {
            var userDto = this._mediatR.Send(command);
            if (userDto == null)
                return BadRequest("Something went wrong.");

            return Ok(userDto);
        }
    }
}