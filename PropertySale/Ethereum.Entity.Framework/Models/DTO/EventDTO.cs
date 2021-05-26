using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Models.DTO
{
    public class EventDTO
    {
        public Event Event { get; set; }
        public string ErrorMessage { get; set; }
    }
}
