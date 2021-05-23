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
        Task<string> AddProperty(string publicUserAccount, Property property);
        //void TestMe<T>(T estatePropertyItem) where T : new();
        void TestMe<T, I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new();
    }
}
