using Ethereum.Entity.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Interfaces
{
    public interface ISmartContractService
    {
        Task DeployPropertySaleContractAsync(string deployerPrivateAccount);
        Task<string> AddPropertyToChainAsync(string accountPrivate, Property propertyObj);
    }
}
