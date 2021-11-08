using System;
using System.Collections.Generic;


namespace BitApp.Models
{
    public partial class TransactionLog
    {
        public int TransactionId { get; set; }
        public int SentAmt { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderAccount { get; set; }
        public string ReceiverAccount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual Customer Receiver { get; set; }
        public virtual Customer Sender { get; set; }
    }
}
