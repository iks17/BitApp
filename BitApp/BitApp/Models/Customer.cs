using System;
using System.Collections.Generic;



namespace BitApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            BusinessAccounts = new List<BusinessAccount>();
            PrivateAccounts = new List<PrivateAccount>();
            TransactionLogReceivers = new List<TransactionLog>();
            TransactionLogSenders = new List<TransactionLog>();
        }

        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public int UserId { get; set; }

      //  public string UserName { get; set; }
        public virtual User User { get; set; }
        public virtual List<BusinessAccount> BusinessAccounts { get; set; }
        public virtual List<PrivateAccount> PrivateAccounts { get; set; }
        public virtual List<TransactionLog> TransactionLogReceivers { get; set; }
        public virtual List<TransactionLog> TransactionLogSenders { get; set; }
    }
}
