namespace Base.Application.Users.Commands.Logins;

public record LoginResponse
{
    public LoginResponse(string Token)
    {
        this.Token = Token;
    }

    public string Token { get; init; }
}
