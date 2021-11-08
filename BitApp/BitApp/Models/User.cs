using System;
using System.Collections.Generic;


namespace BitApp.Models
{
    public partial class User
    {
        public User()
        {
            Admins = new List<Admin>();
            Customers = new List<Customer>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime RegistartionDate { get; set; }

        public virtual List<Admin> Admins { get; set; }
        public virtual List<Customer> Customers { get; set; }
    }
}
