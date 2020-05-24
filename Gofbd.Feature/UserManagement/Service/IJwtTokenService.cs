namespace Gofbd.Feature
{
    using Gofbd.Dto;

    public interface IJwtTokenService
    {
        string GenerateAccessToken(UserDto userDto);

        string GenerateRefreshToken(UserDto userDto);
    }
}
