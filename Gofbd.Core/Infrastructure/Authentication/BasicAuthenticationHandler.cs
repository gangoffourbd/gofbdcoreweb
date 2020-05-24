namespace Gofbd.Core
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ILogger _logger;
        private readonly IDataContext dataContext;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder,
            ISystemClock clock,
            IDataContext dataContext) : base(options, loggerFactory, encoder, clock)
        {
            this._logger = loggerFactory.CreateLogger(typeof(BasicAuthenticationHandler));
            this.dataContext = dataContext;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //this._logger.LogInformation("BasicAuthenticationHanlder:HandleAuthenticationAsync called.");

            //if (!Request.Headers.ContainsKey("Authorization"))
            //    return AuthenticateResult.Fail("Authorization header was not found.");

            //var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            //var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
            //var passedHeaderValue = Encoding.Unicode.GetString(bytes);
            //this._logger.LogInformation($"{passedHeaderValue}");

            //var email = passedHeaderValue.Split(":")[0];
            //var password = passedHeaderValue.Split(":")[1];

            ////authenticate in database
            //var user = this.authenticateService.Authenticate(email, password);
            //if (user == null)
            //    return AuthenticateResult.Fail("Username or password incorrect.");

            //var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
            //var identity = new ClaimsIdentity(claims, Scheme.Name);
            //var principal = new ClaimsPrincipal(identity);
            //var ticket = new AuthenticationTicket(principal, Scheme.Name);

            //return AuthenticateResult.Success(ticket);
            throw new NotImplementedException();
        }
    }
}
