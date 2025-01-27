using Base.Domain.Shared;
using MediatR;

namespace Base.Application.Users.Commands.Logins;

public class LoginRequest : IRequest<Result<LoginResponse>>
{
    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}
