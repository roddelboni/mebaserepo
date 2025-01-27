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
        if (Status == CommandResultStatus.Ok)
        {
            return _value == null ? controller.NoContent() : controller.Ok(_value);
        }

        if (Status == CommandResultStatus.InvalidInput)
        {
            return controller.BadRequest(Errors);
        }

        if (Status == CommandResultStatus.Created)
        {
            return controller.StatusCode(201, _value);
        }

        if (Status == CommandResultStatus.AlreadyExists)
        {
            return controller.BadRequest(Errors);
        }

        if (Status == CommandResultStatus.Unknown)
        {
            return controller.StatusCode(500, Errors);
        }

        if (Status == CommandResultStatus.NotFound)
        {
            return controller.NotFound(Errors);
        }

        if (Status == CommandResultStatus.BusinessError)
        {
            return controller.UnprocessableEntity(Errors);
        }

        throw new InvalidOperationException($"Status {Status} was not mapped to a status code");
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