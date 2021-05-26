using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Models
{
    public class JSONUser
    {
        public int Id { get; set; }
        public string PrivateAddress { get; set; }
        public string PublicAddress { get; set; }
        public string FullName { get; set; }
        public int Type { get; set; }
        public string Ether { get; set; }
        public string Email { get; set; }
    }
}
