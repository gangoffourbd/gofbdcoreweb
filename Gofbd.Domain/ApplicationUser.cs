namespace Gofbd.Domain
{
    using Gofbd.Core.Enums;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public UserType UserTpe { get; set; }

        public UserStatus UserStatus { get; set; }
    }
}
