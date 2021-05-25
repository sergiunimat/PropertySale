using Ethereum.Entity.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Interfaces
{
    public interface IBlockchainEntityFrameworkService
    {
        //Task<string> AddProperty(string publicUserAccount, Property property);
        Task<string> AddEstateProperty<T, I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new();
        Task<string> EditEstateProperty<T, I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new();
        Task<string> DeleteEstateProperty<T, I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new();
        Task<string> TransferProperty<T, I, Z>(T estatePropertyItem, I frameworkSellerUserItem, Z frameworkBuyerUserItem) where T : new() where I : new() where Z : new();
        void TestMe<T, I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new();
    }
}
