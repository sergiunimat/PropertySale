using Ethereum.Entity.Framework.FrameworkDataAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.EstatePropertyAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.UserAnnotations;
using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Ethereum.Entity.Framework.Models.DTO;
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
        private readonly IReflextionService _reflextionService;

        public BlockchainEntityFrameworkService(IDatabaseService databaseService,ISmartContractService smartContractService, IReflextionService reflextionService)
        {
            _databaseService = databaseService;
            _smartContractService = smartContractService;
            _reflextionService = reflextionService;
        }

        /*dont use this fn()!*/
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

        public async Task<string> AddEstateProperty<T,I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new()
        {
            var DTO = _reflextionService.BuildInternalDTO(estatePropertyItem, frameworkUserItem);
            if (DTO.ErrorMessage != ResponseStatus.SUCCESS)
                return DTO.ErrorMessage;
            try
            {
                var user = await _databaseService.GetUserByPublicAddressAsync(DTO.User.PublicAddress);
                var chainResponse = await _smartContractService.AddPropertyToChainAsync(user.PrivateAddress, DTO.Property);
                if (chainResponse == ResponseStatus.SUCCESS)
                {
                    await _databaseService.AddPropertyAsync(DTO.Property);
                    return ResponseStatus.SUCCESS;
                }
                return chainResponse;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> EditEstateProperty<T,I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new()
        {
            var DTO = _reflextionService.BuildInternalDTO(estatePropertyItem, frameworkUserItem);
            if (DTO.ErrorMessage != ResponseStatus.SUCCESS)
                return DTO.ErrorMessage;
            try
            {
                var user = await _databaseService.GetUserByPublicAddressAsync(DTO.User.PublicAddress);
                var chainResponse = await _smartContractService.EditPropertyOnChain(user.PrivateAddress, DTO.Property);
                if (chainResponse == ResponseStatus.SUCCESS)
                {
                    await _databaseService.EditPropertyAsync(DTO.Property);
                    return ResponseStatus.SUCCESS;
                }
                return chainResponse;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> DeleteEstateProperty<T, I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I : new()
        {
            var DTO = _reflextionService.BuildInternalDTO(estatePropertyItem, frameworkUserItem);
            if (DTO.ErrorMessage != ResponseStatus.SUCCESS)
                return DTO.ErrorMessage;
            try
            {
                var user = await _databaseService.GetUserByPublicAddressAsync(DTO.User.PublicAddress);
                var chainResponse = await _smartContractService.DeletePropertyOnChain(user.PrivateAddress, DTO.Property);
                if (chainResponse == ResponseStatus.SUCCESS)
                {
                    await _databaseService.RemovePropertyAsync(DTO.Property.Id);
                    return ResponseStatus.SUCCESS;
                }
                return chainResponse;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /*dont use this fn()!*/
        public void TestMe<T,I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I:new()
        {
            var buildResult = _reflextionService.BuildInternalDTO(estatePropertyItem, frameworkUserItem);
        }
        
    }
}
