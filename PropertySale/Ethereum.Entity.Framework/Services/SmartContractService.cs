using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Ethereum.Entity.Framework.Models.StaticModels;
using Ethereum.Entity.Framework.SmartContracts.PropertySaleContract;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Services
{
    public class SmartContractService: ISmartContractService
    {
        readonly IDatabaseService _databaseService;

        public SmartContractService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /*Service-class responsible for interacting with nethereum*/

        #region connection type
        private async Task<Web3> InitialiseConnectionWithSenderAddress(string accountPrivateAddress) {            
            var externalProject = await _databaseService.GetExternalProjectByIdAsync(1);
            var account = new Account(accountPrivateAddress);
            return new Web3(account, externalProject.ProjectLink);
        }

        private async Task<Web3> InitialiseSimpleConnection()
        {
            var externalProject = await _databaseService.GetExternalProjectByIdAsync(1);            
            return new Web3(externalProject.ProjectLink);
        }
        #endregion

        #region Deploy Contract
        public async Task DeployPropertySaleContractAsync(string deployerPrivateAccount)
        {            
            var web3 = await InitialiseConnectionWithSenderAddress(deployerPrivateAccount);

            #region INFO
            /* * Create the instance of the Smart Contract
               * Here we are hardcoding what smart contract we deploy
               * Note that the smart contract`s BYTECODE is already defined in ContractDeploy class**
             */
            #endregion

            var deployReceipt = new ContractDeploy();
            /*Set the handler*/
            var deploymentHandler = web3.Eth.GetContractDeploymentHandler<ContractDeploy>();
            /*The actual query to deploy the smart contract*/
            var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deployReceipt);
            /*Get the contract address <- we can access it through this*/
            var contractAddress = transactionReceipt.ContractAddress;

            if (contractAddress!=null|| contractAddress !="")
            {
                var newSmartContract = new SmartContract() { 
                    Address = contractAddress,
                    Name = "PropertySale"
                };
                await _databaseService.AddSmartContractAsync(newSmartContract);
            }
        }
        #endregion

        #region Function Mappings
        public async Task<string> AddPropertyToChainAsync(string accountPrivate, Property propertyObj)
        {
            /*get connection*/
            var web3 = await InitialiseConnectionWithSenderAddress(accountPrivate);
            /*get smart contract*/
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);            
            var _addPropertyInterdace = new AddProperty()
            {
                propertyId = propertyObj.Id,
                weiPrice = Web3.Convert.ToWei(propertyObj.Ether)
            };
            try
            {
                var _interfaceHandler = web3.Eth.GetContractTransactionHandler<AddProperty>();
                var transactionReceipt = await _interfaceHandler.SendRequestAndWaitForReceiptAsync(smartContract.Address, _addPropertyInterdace);
                return ResponseStatus.SUCCESS;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> EditPropertyOnChain(string accountPrivate, Property propertyObj) {
            var web3 = await InitialiseConnectionWithSenderAddress(accountPrivate);
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var _editPropertyInstance = new EditPropertyPrice()
            {
                propertyId = propertyObj.Id,
                weiPrice = Web3.Convert.ToWei(propertyObj.Ether)
            };

            try
            {
                var _interfaceHandler = web3.Eth.GetContractTransactionHandler<EditPropertyPrice>();
                var transactionReceipt = await _interfaceHandler.SendRequestAndWaitForReceiptAsync(smartContract.Address, _editPropertyInstance);
                return ResponseStatus.SUCCESS;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> DeletePropertyOnChain(string accountPrivate, Property propertyObj)
        {
            var web3 = await InitialiseConnectionWithSenderAddress(accountPrivate);
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var _deletePropertyInstance = new DeleteProperty()
            {
                propertyId = propertyObj.Id,                
            };

            try
            {
                var _interfaceHandler = web3.Eth.GetContractTransactionHandler<DeleteProperty>();
                var transactionReceipt = await _interfaceHandler.SendRequestAndWaitForReceiptAsync(smartContract.Address, _deletePropertyInstance);
                return ResponseStatus.SUCCESS;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> CheckIfAddressIsOwnerByEstateAccount(Property propertyObj) 
        {

            var web3 = await InitialiseSimpleConnection();            
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var _checkIfAddressIsOwnedByOwnerAccount = new CheckIfAddressIsOwnedByOwnerAccount() {
                propertyId = propertyObj.Id
            };
            try
            {
                var _interfaceHandler = web3.Eth.GetContractQueryHandler<CheckIfAddressIsOwnedByOwnerAccount>();
                var transactionReceipt = await _interfaceHandler.QueryAsync<bool>(smartContract.Address,_checkIfAddressIsOwnedByOwnerAccount);
                if(transactionReceipt==true)
                    return ResponseStatus.SUCCESS;
                return ResponseStatus.FAIL;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> CheckIfPropertyExistsAndisOwnedByTheSeller(string sellerPublicAddress,Property propertyObj)
        {

            var web3 = await InitialiseSimpleConnection();
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var checkIfPropertyExistsAndisOwnedByTheSellerInstance = new CheckIfPropertyExistsAndisOwnedByTheSeller()
            {
                propertyId = propertyObj.Id,
                sellerAddress = sellerPublicAddress
            };
            try
            {
                var _interfaceHandler = web3.Eth.GetContractQueryHandler<CheckIfPropertyExistsAndisOwnedByTheSeller>();
                var transactionReceipt = await _interfaceHandler.QueryAsync<bool>(smartContract.Address, checkIfPropertyExistsAndisOwnedByTheSellerInstance);
                if (transactionReceipt == true)
                    return ResponseStatus.SUCCESS;
                return ResponseStatus.FAIL;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetArrayOfProperties()
        {

            var web3 = await InitialiseSimpleConnection();
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var _getArrayOfPropertiesInstance = new GetArrayOfProperties() { };
            
            try
            {
                var _interfaceHandler = web3.Eth.GetContractQueryHandler<GetArrayOfProperties>();
                var transactionReceipt = await _interfaceHandler.QueryAsync<string>(smartContract.Address, _getArrayOfPropertiesInstance);
                if (transactionReceipt != "" || transactionReceipt != null)
                    return transactionReceipt;
                return ResponseStatus.FAIL;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetOwnerAddress()
        {

            var web3 = await InitialiseSimpleConnection();
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var _getOwnerAddressInstance = new GetOwnerAddress() { };

            try
            {
                var _interfaceHandler = web3.Eth.GetContractQueryHandler<GetOwnerAddress>();
                var transactionReceipt = await _interfaceHandler.QueryAsync<string>(smartContract.Address, _getOwnerAddressInstance);
                if (transactionReceipt != "" || transactionReceipt != null)
                    return transactionReceipt;
                return ResponseStatus.FAIL;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetPropertyEtherPriceByid(Property propertyObj)
        {

            var web3 = await InitialiseSimpleConnection();
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var _getPropertyWeiPriceByidInstance = new GetPropertyWeiPriceByid() { 
                propertyId = propertyObj.Id
            };

            try
            {
                var _interfaceHandler = web3.Eth.GetContractQueryHandler<GetPropertyWeiPriceByid>();
                var transactionReceipt = await _interfaceHandler.QueryAsync<BigInteger>(smartContract.Address, _getPropertyWeiPriceByidInstance);
                if (transactionReceipt != 0)                                    
                    //return transactionReceipt.ToString();
                    return Web3.Convert.FromWei(transactionReceipt).ToString();
                
                return ResponseStatus.FAIL;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> TransferProperty(string accountPrivateSeller, Property propertyObj,string accountPublicBuyer)
        {            
            var web3 = await InitialiseConnectionWithSenderAddress(accountPrivateSeller);
            var smartContract = await _databaseService.GetSmartContractBasedOnIdAsync(1);
            var _transferPropertyInstance = new TransferProperty()
            {
                propertyId = propertyObj.Id,
                to=accountPublicBuyer
            };
            try
            {
                var _interfaceHandler = web3.Eth.GetContractTransactionHandler<TransferProperty>();
                var transactionReceipt = await _interfaceHandler.SendRequestAndWaitForReceiptAsync(smartContract.Address, _transferPropertyInstance);
                return ResponseStatus.SUCCESS;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> TransferEtherFromAccountToAccount(string fromPrivate, string toPublic, string ether)
        {
            var web3 = await InitialiseConnectionWithSenderAddress(fromPrivate);
            try
            {
                var transaction = await web3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(toPublic, Convert.ToDecimal(ether));
                return ResponseStatus.SUCCESS;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        #endregion 
    }
}
