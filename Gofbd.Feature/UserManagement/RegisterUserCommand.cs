namespace Gofbd.Feature
{
    using Gofbd.Core.Enums;
    using Gofbd.Dto;
    using MediatR;

    public class RegisterUserCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }
    }
}
