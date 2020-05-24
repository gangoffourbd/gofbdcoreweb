namespace Gofbd.Feature
{
    using Gofbd.Dto;
    using MediatR;
    public class SignInCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
