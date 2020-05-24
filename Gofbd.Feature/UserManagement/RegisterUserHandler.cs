namespace Gofbd.Feature
{
    using AutoMapper;
    using Gofbd.Core.Enums;
    using Gofbd.Core.Infrastructure.ExtensionMetthod;
    using Gofbd.DataAccess;
    using Gofbd.Domain;
    using Gofbd.Dto;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IDataContextFactory _dataContextFactory;

        public RegisterUserHandler(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IDataContextFactory dataContextFactory)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._dataContextFactory = dataContextFactory;
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserName.IsNullOrEmpty())
                return null;
            var appUser = new ApplicationUser()
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                NormalizedUserName = request.Name,
                UserTpe = request.UserType,
                UserStatus = UserStatus.Active
            };
            var result = await this._userManager.CreateAsync(appUser, request.Password);
            if (!result.Succeeded)
                return null;
            using (var dataContext = await this._dataContextFactory.Create())
            {
                var createdUser = await dataContext.Get<ApplicationUser>().FirstOrDefaultAsync(a => a.UserName == request.UserName);
                var userDto = this._mapper.Map<ApplicationUser, UserDto>(createdUser);
                return userDto;
            }
        }
    }
}
