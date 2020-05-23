namespace Gofbd.Feature
{
    using Gofbd.Dto;
    using MediatR;
    public class SignInCommand : IRequest<UserDto>
    {
        public string UserId { get; set; }

        public string Password { get; set; }
    }
}
