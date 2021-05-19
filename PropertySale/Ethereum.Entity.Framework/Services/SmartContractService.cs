using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Ethereum.Entity.Framework.Models.StaticModels;
using Ethereum.Entity.Framework.SmartContracts.PropertySaleContract;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
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


        #endregion 
    }
}
