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
    }
}
