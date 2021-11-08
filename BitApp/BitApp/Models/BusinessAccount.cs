using System;
using System.Collections.Generic;


namespace BitApp.Models
{
    public partial class BusinessAccount
    {
        public BusinessAccount()
        {
            Loans = new List<Loan>();
        }

        public string AccountId { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPassword { get; set; }
        public string BusinessName { get; set; }
        public int ActiveManagersNum { get; set; }
        public string BusibessAdress { get; set; }
        public int AnualIncome { get; set; }
        public int NetWorth { get; set; }
        public DateTime BusinessOpenDate { get; set; }
        public string MainCurrency { get; set; }
        public double TotalBalance { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<Loan> Loans { get; set; }
    }
}
