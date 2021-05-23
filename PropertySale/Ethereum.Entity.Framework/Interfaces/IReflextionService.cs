using Ethereum.Entity.Framework.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Interfaces
{
    public interface IReflextionService
    {
        UserPropertyDTO BuildInternalDTO<T,I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I:new();
    }
}
