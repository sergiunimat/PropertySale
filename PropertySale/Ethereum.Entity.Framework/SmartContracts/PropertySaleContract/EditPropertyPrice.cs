using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ethereum.Entity.Framework.SmartContracts.PropertySaleContract
{
    [Function("editPropertyPrice")]
    public class EditPropertyPrice : FunctionMessage
    {
        [Parameter("string", "_propertyId", 1)]
        public string propertyId { get; set; }
        [Parameter("uint", "_weiPrice", 2)]
        public BigInteger weiPrice { get; set; }
    }
}
