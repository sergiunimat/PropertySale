using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Models
{
    public class JSONEvent
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Type { get; set; } 
        public string Message { get; set; }
        public string UserName { get; set; }   
    }
}
