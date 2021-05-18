using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ethereum.Entity.Framework.SmartContracts.PropertySaleContract
{
    [Function("transferProperty", "bool")]
    public class TransferProperty : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public string to { get; set; }
        [Parameter("string", "_propertyId", 2)]
        public string propertyId { get; set; }
    }
}
