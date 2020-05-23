namespace Gofbd.Dto
{
    using Gofbd.Core.Enums;
    using System;

    [Serializable]
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
