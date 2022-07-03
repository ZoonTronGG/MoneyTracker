using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker
{
   
    public class Transaction
    { 
        public double Amount { get; set; }
        public Category Category { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }
    }
}
