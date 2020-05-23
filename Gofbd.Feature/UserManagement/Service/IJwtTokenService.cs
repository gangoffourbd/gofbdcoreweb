namespace Gofbd.Feature
{
    using Gofbd.Dto;

    public interface IJwtTokenService
    {
        string CreateAccessToken(UserDto userDto);

        string CreateRefreshToken(UserDto userDto);
    }
}
