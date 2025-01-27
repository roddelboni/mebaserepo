using Base.Data.Auth;
using Base.Domain.Interfaces;
using Base.Domain.Shared;
using MediatR;

namespace Base.Application.Users.Commands.Logins
{
    public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            var user = await _userRepository.Login(request.Email, hash, cancellationToken);

            if (user is null)
            {
                var error = Result<LoginResponse>.Failure(Error.BigError);

                return error;
            }

            var token = _authService.GenerateToken(user.Email, user.Role);

            var model = new LoginResponse(Token:token);

            return Result<LoginResponse>.Sucess(model, CommandResultStatus.Ok);
        }
    }
}
