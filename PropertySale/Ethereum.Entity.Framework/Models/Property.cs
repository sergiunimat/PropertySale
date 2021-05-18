using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Models
{
    public class Property
    {
        [Key]
        public string Id { get; set; }
        public BigInteger wei { get; set; }
        public string OwnerPublicAddress { get; set; }        
        public string Description { get; set; }
        public string GeographicalAddress { get; set; }
    }
}
