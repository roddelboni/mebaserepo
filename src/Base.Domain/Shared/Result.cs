using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;


namespace Base.Domain.Shared;

public class Result<T>
{
    private readonly T _value;
    public bool IsValid {get;}
    public bool NotValid => !IsValid;
    public Error Error { get; }

    [JsonIgnore]
    public CommandResultStatus Status { get; }
    public IEnumerable<Error> Errors { get; }

    public Result(T? value, bool isValid, Error error)
    {
        _value = value;
        IsValid = isValid;
        Error = error;      ;
    }

    public Result(T? value, bool isValid, Error error, CommandResultStatus? status)
    {
        _value = value;
        IsValid = isValid;
        Error = error;
        Status = status.Value;
    }

    public static Result<T> Sucess(T value) => new(value, true, Error.None);
    public static Result<T> Sucess(T value, CommandResultStatus status) => new(value, true, Error.None, CommandResultStatus.Ok);
    public static Result<T> Failure(Error error) => new Result<T>(default, false, error);

    public T Value => IsValid ? _value : default;
    public static implicit operator Result<T>(T value) => Sucess(value);
    public static explicit operator T?(Result<T> value) => value.Value;

public IActionResult ToActionResult(ControllerBase controller)
{
    return Status switch
    {
        CommandResultStatus.Ok => _value == null ? controller.NoContent() : controller.Ok(_value),
        CommandResultStatus.InvalidInput => controller.BadRequest(Errors),
        CommandResultStatus.Created => controller.StatusCode(201, _value),
        CommandResultStatus.AlreadyExists => controller.BadRequest(Errors),
        CommandResultStatus.Unknown => controller.StatusCode(500, Errors),
        CommandResultStatus.NotFound => controller.NotFound(Errors),
        CommandResultStatus.BusinessError => controller.UnprocessableEntity(Errors),
        _ => throw new InvalidOperationException($"Status {Status} was not mapped to a status code")
    };
}
}

public enum CommandResultStatus
{
    Unknown = 0,
    Ok = 1,
    AlreadyExists = 2,
    InvalidInput = 3,
    NotFound = 4,
    BusinessError = 5,
    Created = 6,
    Updated = 7
}
