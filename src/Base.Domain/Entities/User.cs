using Base.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities
{
    public class User : Entity
    { 
        public User() { }
        public User(string email, bool active, string pass, string role) :base() 
        {            
            Email = email;
            Active = active;
            Password = pass;
            Role = role;
        }    
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public long CustomerId { get; private set; }
        public Customer Customer { get; set; }

        public void AddCustomer(Customer customer)
        {
            Customer = customer;
        }
    }
}
