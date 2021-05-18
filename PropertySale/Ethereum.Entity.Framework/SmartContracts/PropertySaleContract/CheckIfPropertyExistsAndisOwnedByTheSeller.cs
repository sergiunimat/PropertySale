using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ethereum.Entity.Framework.SmartContracts.PropertySaleContract
{
    [Function("checkIfPropertyExistsAndisOwnedByTheSeller", "bool")]
    public class CheckIfPropertyExistsAndisOwnedByTheSeller : FunctionMessage
    {
        [Parameter("string", "_propertyId", 1)]
        public string propertyId { get; set; }
        [Parameter("address", "_sellerAddress", 2)]
        public string sellerAddress { get; set; }
    }
}
