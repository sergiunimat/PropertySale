using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Models
{
    public class JSONProperty
    {
        public string Id { get; set; }
        public string Ether { get; set; }
        public string OwnerPublicAddress { get; set; }
        public string Description { get; set; }
        public string GeographicalAddress { get; set; }
    }
}
