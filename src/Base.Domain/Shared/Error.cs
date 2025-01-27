namespace Base.Domain.Shared;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error BigError = new("code", "Motive");
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get;}
    public string Message { get; } 
}
