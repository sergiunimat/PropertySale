using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Models
{
    class EventWithUser
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Type { get; set; } 
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}
