using System;
using System.Collections.Generic;


namespace BitApp.Models
{
    public partial class PrivateAccount
    {
        public PrivateAccount()
        {
            Loans = new List<Loan>();
        }

        public string AccountId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MainCurrency { get; set; }
        public int AnualIncome { get; set; }
        public double TotalBalance { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<Loan> Loans { get; set; }
    }
}
