namespace Gofbd.Feature
{
    using Gofbd.Core.Infrastructure.ExtensionMetthod;
    using Gofbd.DataAccess;
    using Gofbd.Dto;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SignInHandler : IRequestHandler<SignInCommand, UserDto>
    {
        private readonly IDataContextFactory _dataContextFactory;
        public SignInHandler(IDataContextFactory dataContextFactory)
        {
            this._dataContextFactory = dataContextFactory;
        }

        public async Task<UserDto> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            if (command.UserId.IsNullOrEmpty())
                return null;

            if (command.Password.IsNullOrEmpty())
                return null;

            using (var dataContext = await this._dataContextFactory.Create()) 
            {

            }

            return null;
        }
    }
}
