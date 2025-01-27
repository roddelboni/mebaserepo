using Base.Domain.Shared;
using MediatR;

namespace Base.Application.Users.Commands.CreateUser
{
    public class CreateUserRequest : IRequest<Result<CreateUserResponse>>
    {
        public string FullName { get; init; }
        public string Email { get; init; }
        public DateOnly BirthDate { get; init; }
        public string Document {  get; init; }
        public string Password { get; init; }
        public string Role { get; init; }
    }
}
