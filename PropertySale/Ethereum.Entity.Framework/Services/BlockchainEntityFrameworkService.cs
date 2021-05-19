using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Ethereum.Entity.Framework.Models.StaticModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Services
{
    public class BlockchainEntityFrameworkService: IBlockchainEntityFrameworkService
    {
        private readonly IDatabaseService _databaseService;
        private readonly ISmartContractService _smartContractService;

        public BlockchainEntityFrameworkService(IDatabaseService databaseService,ISmartContractService smartContractService)
        {
            _databaseService = databaseService;
            _smartContractService = smartContractService;
        }

        public async Task<string> AddProperty(string publicUserAccount,Property property) {
            /*
             1. add to blockchain if succed then add to db
             */
            try
            {
                var user = await _databaseService.GetUserByPublicAddressAsync(publicUserAccount);
                var chainResponse = await _smartContractService.AddPropertyToChainAsync(user.PrivateAddress, property);
                if (chainResponse==ResponseStatus.SUCCESS)
                {
                    await _databaseService.AddPropertyAsync(property);
                    return ResponseStatus.SUCCESS;
                }
                return chainResponse;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }
    }
}
