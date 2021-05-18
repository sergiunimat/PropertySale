using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Type { get; set; } //1-> success, 0-> fail, 2-> transaction
        public string Message { get; set; }
        public string UserPublicAddress { get; set; }   //who triggered it
    }
}
