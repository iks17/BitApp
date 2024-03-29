﻿using System;
using System.Collections.Generic;


namespace BitApp.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Loans = new List<Loan>();
        }

        public int AdminId { get; set; }
        public int EnrolledCustomers { get; set; }
        public DateTime LastOnline { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual List<Loan> Loans { get; set; }
    }
}
