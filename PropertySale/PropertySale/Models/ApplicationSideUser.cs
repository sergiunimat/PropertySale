using Ethereum.Entity.Framework.FrameworkDataAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.UserAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Models
{
    [FrameworkUser]
    public class ApplicationSideUser
    {
        [FrameworkUserIdInt]
        public int ExternalUserUserId { get; set; }
        [FrameworkUserEmailString]
        public string ExternalUserEmail { get; set; }
        [FrameworkUserEtherString]
        public string ExternalUserEther { get; set; }
        [FrameworkUserFullNameString]
        public string ExternalUserFullName { get; set; }
        [FrameworkUserPrivateAddressString]
        public string ExternalUserPrivateAddress { get; set; }
        [FrameworkUserPublicAddressString]
        public string ExternalUserPublicAddress { get; set; }
        [FrameworkUserTypeInt]
        public int ExternalUserType { get; set; }
    }
}
