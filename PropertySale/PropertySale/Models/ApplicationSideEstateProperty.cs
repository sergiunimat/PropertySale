using Ethereum.Entity.Framework.FrameworkDataAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.EstatePropertyAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Models
{
    [EstateProperty]
    public class ApplicationSideEstateProperty
    {
        [EstatePropertyIdString]
        public string EstatePropertyId { get; set; }
        [EstatePropertyDescriptionString]
        public string EstatePropertyDescription { get; set; }
        [EstatePropertyEtherString]
        public string EstatePropertyEther { get; set; }
        [EstatePropertyOwnerPublicAddressString]
        public string EstatePropertyOwnerAddress { get; set; }
        [EstatePropetyGeographicalAddressString]
        public string EstatePropertyGeoAddress { get; set; }
        public string AdditionalExternalAppProperty { get; set; }
    }
}
