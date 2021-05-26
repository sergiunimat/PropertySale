using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Models.DTOs
{
    public class ViewListsDTO
    {
        public List<JSONUser> Users { get; set; }
        public List<JSONProperty> Properties { get; set; }
        public List<JSONEvent> Events { get; set; }
    }
}
