using System;
using System.Collections.Generic;
using System.Text;

namespace BitApp.Models
{
    public partial class Card
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Cvc { get; set; }
        public int UserId { get; set; }

    }
}
