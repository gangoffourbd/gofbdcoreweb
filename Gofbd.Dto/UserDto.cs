namespace Gofbd.Dto
{
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using Gofbd.Core.Enums;
    using Gofbd.Domain;
    using System;

    [Serializable]
    [AutoMap(typeof(ApplicationUser))]
    public class UserDto
    {
        [SourceMember(nameof(ApplicationUser.Id))]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Ignore]
        public string Password { get; set; }

        public UserType UserType { get; set; }

        public UserStatus UserStatus { get; set; }

        [Ignore]
        public string AccessToken { get; set; }

        [Ignore]
        public string RefreshToken { get; set; }
    }
}
