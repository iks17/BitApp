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
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistartionDate { get; set; }
      
        public virtual List<Admin> Admins { get; set; }
        public virtual List<Customer> Customers { get; set; }
    }
}
