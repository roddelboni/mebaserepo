using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Users.Commands.CreateUser;

public class CreateUserResponse
{
    public CreateUserResponse(string email)
    {
        Email = email;
    }

    public string Email { get; set; } 
}

public static class CreateUserResponseExtension
{
    public static CreateUserResponse CreateUserToResponse(this Base.Domain.Entities.User user)
    {
        return new(user.Email);
    }
}



