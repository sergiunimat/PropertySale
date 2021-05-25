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
        Task<string> EditPropertyOnChain(string accountPrivate, Property propertyObj);
        Task<string> DeletePropertyOnChain(string accountPrivate, Property propertyObj);
        Task<string> CheckIfAddressIsOwnerByEstateAccount(Property propertyObj);
        Task<string> CheckIfPropertyExistsAndisOwnedByTheSeller(string sellerPublicAddress, Property propertyObj);
        Task<string> GetArrayOfProperties();
        Task<string> GetOwnerAddress();
        Task<string> GetPropertyWeiPriceByid(Property propertyObj);
        Task<string> TransferProperty(string accountPrivateSeller, Property propertyObj, string accountPublicBuyer);
    }
}
