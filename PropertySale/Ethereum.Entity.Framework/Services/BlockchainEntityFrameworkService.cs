using Ethereum.Entity.Framework.FrameworkDataAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.EstatePropertyAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.UserAnnotations;
using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Ethereum.Entity.Framework.Models.DTO;
using Ethereum.Entity.Framework.Models.StaticModels;
using Nethereum.ABI.Util;
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
        //public async Task<string> AddProperty(string publicUserAccount,Property property) {
        //    /*
        //     1. add to blockchain if succed then add to db
        //     */
        //    try
        //    {
        //        var user = await _databaseService.GetUserByPublicAddressAsync(publicUserAccount);
        //        var chainResponse = await _smartContractService.AddPropertyToChainAsync(user.PrivateAddress, property);
        //        if (chainResponse==ResponseStatus.SUCCESS)
        //        {
        //            await _databaseService.AddPropertyAsync(property);
        //            return ResponseStatus.SUCCESS;
        //        }
        //        return chainResponse;
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
            
        //}

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

        public async Task<string> TransferProperty<T,I,Z>(T estatePropertyItem, I frameworkSellerUserItem, Z frameworkBuyerUserItem) where T : new() where I : new() where Z : new()
        {
            var DTO = _reflextionService.BuildInternalTransferPropertyDTO(estatePropertyItem, frameworkSellerUserItem, frameworkBuyerUserItem);
            if (DTO.ErrorMessage != ResponseStatus.SUCCESS)
                return DTO.ErrorMessage;
            var sellerPrivate = await _databaseService.GetUserByPublicAddressAsync(DTO.Seller.PublicAddress);
            var buyerPrivate = await _databaseService.GetUserByPublicAddressAsync(DTO.Buyer.PublicAddress);
            try
            {
                var checkIfPropertyExistAndIsOwnedByTheSeller = await _smartContractService.CheckIfPropertyExistsAndisOwnedByTheSeller(DTO.Seller.PublicAddress,DTO.Property);
                if (checkIfPropertyExistAndIsOwnedByTheSeller == ResponseStatus.SUCCESS)
                {
                    var checkIfPropertyIsOwnedByEstateCompany = await _smartContractService.CheckIfAddressIsOwnerByEstateAccount(DTO.Property);
                    /*BUY FROM USER*/
                    if (checkIfPropertyIsOwnedByEstateCompany == ResponseStatus.FAIL)
                    {                        
                        var ownerAccount = await _smartContractService.GetOwnerAddress();
                        var propertyEtherValue = await _smartContractService.GetPropertyEtherPriceByid(DTO.Property);

                        /*  95%  */
                        var ninentyFive = (0.95 * Convert.ToDouble(propertyEtherValue)).ToString();
                        var etherTransferReceiptToUser = await _smartContractService.TransferEtherFromAccountToAccount(buyerPrivate.PrivateAddress, DTO.Seller.PublicAddress, ninentyFive);
                        
                        /*  5%  */
                        var five = (0.05 * Convert.ToDouble(propertyEtherValue)).ToString();
                        var etherTransferReceiptToEstateCompany = await _smartContractService.TransferEtherFromAccountToAccount(buyerPrivate.PrivateAddress, ownerAccount, five);
                                                
                        if (etherTransferReceiptToUser == ResponseStatus.SUCCESS && etherTransferReceiptToEstateCompany == ResponseStatus.SUCCESS)
                        {
                            var response= await _smartContractService.TransferProperty(sellerPrivate.PrivateAddress, DTO.Property, DTO.Buyer.PublicAddress);
                            /*change data in the database based on the incoming response from the blockchain.*/
                            if (response == ResponseStatus.SUCCESS)
                            {
                                var newProperty = DTO.Property;
                                newProperty.OwnerPublicAddress = DTO.Buyer.PublicAddress;
                                await _databaseService.EditTransferPropertyAsync(newProperty);
                                return ResponseStatus.SUCCESS;
                            }
                            return response;
                        }
                        return ResponseStatus.FAIL;//meaning that either one or both transfers failed.
                    }

                    /*BUY FROM OWNER*/
                    if (checkIfPropertyIsOwnedByEstateCompany == ResponseStatus.SUCCESS)
                    {
                        /*This is the case where the property is owned by USER - perform 1 transactions.*/
                        var propertyEtherValue = await _smartContractService.GetPropertyEtherPriceByid(DTO.Property);
                        var etherTransferReceipt = await _smartContractService.TransferEtherFromAccountToAccount(buyerPrivate.PrivateAddress, DTO.Seller.PublicAddress, propertyEtherValue);
                        if (etherTransferReceipt == ResponseStatus.SUCCESS) { 
                            var response = await _smartContractService.TransferProperty(sellerPrivate.PrivateAddress,DTO.Property,DTO.Buyer.PublicAddress);
                            if (response==ResponseStatus.SUCCESS)
                            {
                                var newProperty = DTO.Property;
                                newProperty.OwnerPublicAddress = DTO.Buyer.PublicAddress;
                                await _databaseService.EditTransferPropertyAsync(newProperty);
                                return ResponseStatus.SUCCESS;
                            }
                            return response;
                        }                        
                        return etherTransferReceipt;                        
                    }                    
                    /*if not successfull, forward-up the error message.*/
                    return checkIfPropertyIsOwnedByEstateCompany;
                }
                /*if not successfull, forward-up the error message.*/
                return checkIfPropertyExistAndIsOwnedByTheSeller;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task AddEvent<T>(T eventItem) where T : new()
        {
            var DTO = _reflextionService.BuildInternalEventDTO(eventItem);
            try
            {
                if (DTO.ErrorMessage == ResponseStatus.SUCCESS)
                    await _databaseService.AddEventAsync(DTO.Event);
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
