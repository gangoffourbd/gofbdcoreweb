namespace Gofbd.Feature
{
    using AutoMapper;
    using Gofbd.Core.Infrastructure.ExtensionMetthod;
    using Gofbd.DataAccess;
    using Gofbd.Domain;
    using Gofbd.Dto;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SignInHandler : IRequestHandler<SignInCommand, UserDto>
    {
        private readonly IDataContextFactory _dataContextFactory;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;

        public SignInHandler(IDataContextFactory dataContextFactory,
            IMapper mapper,
            IJwtTokenService jwtTokenService)
        {
            this._dataContextFactory = dataContextFactory;
            this._mapper = mapper;
            this._jwtTokenService = jwtTokenService;
        }

        public async Task<UserDto> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            if (command.UserName.IsNullOrEmpty())
                return null;

            if (command.Password.IsNullOrEmpty())
                return null;

            using (var dataContext = await this._dataContextFactory.Create())
            {
                var user = dataContext.Get<ApplicationUser>().SingleOrDefault(s => s.UserName == command.UserName);
                if (user == null)
                    return null;
                var userDto = this._mapper.Map<ApplicationUser, UserDto>(user);
                userDto.AccessToken = this._jwtTokenService.GenerateAccessToken(userDto);
                userDto.RefreshToken = this._jwtTokenService.GenerateRefreshToken(userDto);
                return userDto;
            }
        }
    }
}
