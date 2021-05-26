using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PropertySale.Models;
using PropertySale.Models.DTOs;
using PropertySale.Models.StaticModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PropertySale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseService _databaseService;
        private readonly ISmartContractService _smartContractService;
        private readonly IBlockchainEntityFrameworkService _blockchainEntityFrameworkService;

        public HomeController(ILogger<HomeController> logger, IDatabaseService databaseService, ISmartContractService smartContractService, IBlockchainEntityFrameworkService blockchainEntityFrameworkService)
        {
            _logger = logger;
            _databaseService = databaseService;
            _smartContractService = smartContractService;
            _blockchainEntityFrameworkService = blockchainEntityFrameworkService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            #region One Time Runn
            /*Add users*/
            //var owner = new User()
            //{                
            //    Ether="1",
            //    Email="owner.mail@mail.com",
            //    FullName = "Property Sale Ltd.",
            //    Type = 1,
            //    PublicAddress = "0x836A666a4febd5C4BA19a1898e590492A92e97D7",
            //    PrivateAddress= "8f5370e51350f2c3b2a34a79c9b7e4f5d6899a02ae7db3d47feadee532073c38"
            //};

            //var luke = new User()
            //{
            //    Ether = "1",
            //    Email = "luke.skywalker@mail.com",
            //    FullName = "Luke Skywalker",
            //    Type = 0,
            //    PublicAddress = "0x338CCd662EB8E8c647710f99F4B8fDdf0Ec1887C",
            //    PrivateAddress = "3f92f77e0d353daaf3e2822ca4d01a0bccd5f6401a8752b0746cf20df78b7e26"
            //};
            //var chewbacca = new User()
            //{
            //    Ether = "1",
            //    Email = "chewbacca@mail.com",
            //    FullName = "Chewbacca",
            //    Type = 0,
            //    PublicAddress = "0x114Cb4183C66cBBd38864EaD3014Dd2c6648c32C",
            //    PrivateAddress = "93bf9fcbf3f0931f4f254313b3abda7bab4eb0775bec178facfda2f6d5ee1584"
            //};
            //var yoda = new User()
            //{
            //    Ether = "1",
            //    Email = "yoda@mail.com",
            //    FullName = "Yoda",
            //    Type = 0,
            //    PublicAddress = "0xc4D3e872ee67a35C0d257D8899C6d1A7cDA49F0a",
            //    PrivateAddress = "f9ae9db4d25260f4161a9e3df391b69bf76c17abd5dadfa07f7bff01e6475297"
            //};
            //await _databaseService.AddUserAsync(owner);
            //await _databaseService.AddUserAsync(luke);
            //await _databaseService.AddUserAsync(chewbacca);
            //await _databaseService.AddUserAsync(yoda);

            /*Add external Project*/
            //var exProject = new ExternalProject() 
            //{ 
            //    Name= "PlaygroundNode",
            //    ProjectId = "78bd716a98df40dd8f9d6e669254041a",
            //    ProjectSecret = "4b76b233d26249d890221aef4a3ceb24",
            //    ProjectLink = "https://rinkeby.infura.io/v3/78bd716a98df40dd8f9d6e669254041a"
            //};
            //await _databaseService.AddExternalProjectAsync(exProject);

            /*Deploy the project*/
            //await _smartContractService.DeployPropertySaleContractAsync("8f5370e51350f2c3b2a34a79c9b7e4f5d6899a02ae7db3d47feadee532073c38");
            #endregion

            /* MOCKING WHAT COMES FROM FRONT END*/
            var publicUserAddress = "0x836A666a4febd5C4BA19a1898e590492A92e97D7";
            var publicUserAddressII = "0x338CCd662EB8E8c647710f99F4B8fDdf0Ec1887C";
            var propertyExampleObject = new Property() {
                Id = "1",//this is a value that we will test!!
                Description = "this property is a testing property and it has dummy values",
                Ether = "2",
                GeographicalAddress = "Death Start Cell 77",
                OwnerPublicAddress = publicUserAddress
            };
            var propertyExampleObjectII = new Property()
            {
                Id = "3",//this is a value that we will test!!
                Description = "this property is a testing property and it has dummy values",
                Ether = "2",
                GeographicalAddress = "Death Start Cell 77",
                OwnerPublicAddress = publicUserAddressII
            };
            ////var result = await _blockchainEntityFrameworkService.AddProperty(publicUserAddress, propertyExampleObject);
            //var resultb = await _blockchainEntityFrameworkService.AddProperty(publicUserAddressII, propertyExampleObjectII);
            ////var a = result;
            //var b = resultb;



           
            var chewbaccaUser = new ApplicationSideUser()
            {
                ExternalUserEmail= "chewbacca@mail.com",
                ExternalUserEther="1",
                ExternalUserFullName= "Chewbacca",
                ExternalUserPrivateAddress = "93bf9fcbf3f0931f4f254313b3abda7bab4eb0775bec178facfda2f6d5ee1584",
                ExternalUserPublicAddress = "0x114Cb4183C66cBBd38864EaD3014Dd2c6648c32C",
                ExternalUserType = 0,
                //ExternalUserUserId = 1                
            };
            var lukeUser = new ApplicationSideUser()
            {
                ExternalUserEmail = "luke.skywalker@mail.com",
                ExternalUserEther = "1",
                ExternalUserFullName = "Luke Skywalker",
                ExternalUserPrivateAddress = "3f92f77e0d353daaf3e2822ca4d01a0bccd5f6401a8752b0746cf20df78b7e26",
                ExternalUserPublicAddress = "0x338CCd662EB8E8c647710f99F4B8fDdf0Ec1887C",
                ExternalUserType = 0,
                //ExternalUserUserId = 1                
            };
            var yodaUser = new ApplicationSideUser()
            {
                ExternalUserEmail = "yoda@mail.com",
                ExternalUserEther = "1",
                ExternalUserFullName = "Yoda",
                ExternalUserPrivateAddress = "f9ae9db4d25260f4161a9e3df391b69bf76c17abd5dadfa07f7bff01e6475297",
                ExternalUserPublicAddress = "0xc4D3e872ee67a35C0d257D8899C6d1A7cDA49F0a",
                ExternalUserType = 0,
                //ExternalUserUserId = 1                
            };
            var ownerUser = new ApplicationSideUser()
            {
                ExternalUserEmail = "owner.mail@mail.com",
                ExternalUserEther = "1",
                ExternalUserFullName = "Property Sale Ltd.",
                ExternalUserPrivateAddress = "8f5370e51350f2c3b2a34a79c9b7e4f5d6899a02ae7db3d47feadee532073c38",
                ExternalUserPublicAddress = "0x836A666a4febd5C4BA19a1898e590492A92e97D7",
                ExternalUserType = 1,
                //ExternalUserUserId = 1                
            };
            var externalEstateProperty = new ApplicationSideEstateProperty() { 
                AdditionalExternalAppProperty = "additional data",
                EstatePropertyDescription = "external obj description",
                EstatePropertyEther = "0.5",
                EstatePropertyGeoAddress = "The moon",
                EstatePropertyId = "111",
                EstatePropertyOwnerAddress = "0x836A666a4febd5C4BA19a1898e590492A92e97D7"
            };
            //_blockchainEntityFrameworkService.TestMe(externalEstateProperty, externalUser);
            //var response = await _blockchainEntityFrameworkService.AddEstateProperty(externalEstateProperty, externalUserOwner);
            //var response = await _blockchainEntityFrameworkService.EditEstateProperty(externalEstateProperty, ownerUser);
            //var response = await _blockchainEntityFrameworkService.AddEstateProperty(externalEstateProperty, ownerUser);
            //var response = await _blockchainEntityFrameworkService.TransferProperty(externalEstateProperty, yodaUser, chewbaccaUser);


            var viewObj = new ViewListsDTO() { 
                Events = JsonSerializer.Deserialize<List<JSONEvent>>(await _databaseService.JSONGetAllEventsAsync()),
                Properties = JsonSerializer.Deserialize<List<JSONProperty>>(await _databaseService.JSONGetAllPropertiesAsync()),
                Users = JsonSerializer.Deserialize<List<JSONUser>>(await _databaseService.JSONGetAllUsersAsync())
            };

            return View(viewObj);
        }

        [HttpPost]
        public async Task<IActionResult> BuyProperty(string buyerPublic,string propertyId, string sellerPublic) {
            var appUserBuyer = new ApplicationSideUser()
            {
                ExternalUserEmail="",
                ExternalUserEther="",
                ExternalUserFullName="",
                ExternalUserPrivateAddress="",
                ExternalUserPublicAddress = buyerPublic,
                ExternalUserType=0,
                ExternalUserUserId=0
            };
            var appUserSeller = new ApplicationSideUser()
            {
                ExternalUserEmail = "",
                ExternalUserEther = "",
                ExternalUserFullName = "",
                ExternalUserPrivateAddress = "",
                ExternalUserPublicAddress = sellerPublic,
                ExternalUserType = 0,
                ExternalUserUserId = 0
            };
            var appProperty = new ApplicationSideEstateProperty() {
                AdditionalExternalAppProperty="",
                EstatePropertyDescription="",
                EstatePropertyEther="",
                EstatePropertyGeoAddress="",
                EstatePropertyId = propertyId,
                EstatePropertyOwnerAddress =""
            };

            try
            {
                var response = await _blockchainEntityFrameworkService.TransferProperty(appProperty, appUserSeller, appUserBuyer);
                if (response==FrameworkResponseStatus.SUCCESS)
                {
                    var applicationEvent = new ApplicationSideEvent() {
                        Message = "The property was successfully transfered from" + appUserSeller.ExternalUserPublicAddress + " to " + appUserBuyer.ExternalUserPublicAddress,
                        TimeStamp = DateTime.Now,
                        Type = 1,
                        UserPublicAddress = appUserBuyer.ExternalUserPublicAddress                        
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                }
                else
                {
                    var applicationEvent = new ApplicationSideEvent()
                    {
                        Message = response,
                        TimeStamp = DateTime.Now,
                        Type = 0,
                        UserPublicAddress = appUserBuyer.ExternalUserPublicAddress
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                }
                /*add event in any case!*/
                return View(IndexAsync());
            }
            catch (Exception)
            {
                return View(IndexAsync());
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(string buyerPublic, string propertyId,string etherValue)
        {
            var appUser = new ApplicationSideUser()
            {
                ExternalUserEmail = "",
                ExternalUserEther = "",
                ExternalUserFullName = "",
                ExternalUserPrivateAddress = "",
                ExternalUserPublicAddress = buyerPublic,
                ExternalUserType = 0,
                ExternalUserUserId = 0
            };
            var appProperty = new ApplicationSideEstateProperty()
            {
                AdditionalExternalAppProperty = "",
                EstatePropertyDescription = "",
                EstatePropertyEther = etherValue,
                EstatePropertyGeoAddress = "",
                EstatePropertyId = propertyId,
                EstatePropertyOwnerAddress = ""
            };

            try
            {
                var response = await _blockchainEntityFrameworkService.EditEstateProperty(appProperty, appUser);
                if (response == FrameworkResponseStatus.SUCCESS)
                {
                    var applicationEvent = new ApplicationSideEvent()
                    {
                        Message = "The property with id ("+appProperty.EstatePropertyId+") was successfully edited.",
                        TimeStamp = DateTime.Now,
                        Type = 1,
                        UserPublicAddress = appUser.ExternalUserPublicAddress
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                }
                else
                {
                    var applicationEvent = new ApplicationSideEvent()
                    {
                        Message = response,
                        TimeStamp = DateTime.Now,
                        Type = 0,
                        UserPublicAddress = appUser.ExternalUserPublicAddress
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                }
                /*add event in any case!*/
                return View(IndexAsync());
            }
            catch (Exception)
            {
                return View(IndexAsync());
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProperty(string buyerPublic, string propertyId)
        {
            var appUser = new ApplicationSideUser()
            {
                ExternalUserEmail = "",
                ExternalUserEther = "",
                ExternalUserFullName = "",
                ExternalUserPrivateAddress = "",
                ExternalUserPublicAddress = buyerPublic,
                ExternalUserType = 0,
                ExternalUserUserId = 0
            };
            var appProperty = new ApplicationSideEstateProperty()
            {
                AdditionalExternalAppProperty = "",
                EstatePropertyDescription = "",
                EstatePropertyEther = "",
                EstatePropertyGeoAddress = "",
                EstatePropertyId = propertyId,
                EstatePropertyOwnerAddress = ""
            };

            try
            {
                var response = await _blockchainEntityFrameworkService.DeleteEstateProperty(appProperty, appUser);
                if (response == FrameworkResponseStatus.SUCCESS)
                {
                    var applicationEvent = new ApplicationSideEvent()
                    {
                        Message = "The property with id (" + appProperty.EstatePropertyId + ") was successfully edited.",
                        TimeStamp = DateTime.Now,
                        Type = 1,
                        UserPublicAddress = appUser.ExternalUserPublicAddress
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                }
                else
                {
                    var applicationEvent = new ApplicationSideEvent()
                    {
                        Message = response,
                        TimeStamp = DateTime.Now,
                        Type = 0,
                        UserPublicAddress = appUser.ExternalUserPublicAddress
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                }
                /*add event in any case!*/
                return View(IndexAsync());
            }
            catch (Exception)
            {
                return View(IndexAsync());
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(ApplicationSideEstateProperty applicationSideEstateProperty) {

            var properyObj = applicationSideEstateProperty;
            properyObj.EstatePropertyId = Guid.NewGuid().ToString();
            var appUser = new ApplicationSideUser()
            {
                ExternalUserEmail = "",
                ExternalUserEther = "",
                ExternalUserFullName = "",
                ExternalUserPrivateAddress = "",
                ExternalUserPublicAddress = applicationSideEstateProperty.EstatePropertyOwnerAddress,
                ExternalUserType = 0,
                ExternalUserUserId = 0
            };

            try
            {
                var response = await _blockchainEntityFrameworkService.AddEstateProperty(properyObj, appUser);
                if (response == FrameworkResponseStatus.SUCCESS)
                {
                    var applicationEvent = new ApplicationSideEvent()
                    {
                        Message = "A new estate property was successfully added with the  id (" + properyObj.EstatePropertyId + ").",
                        TimeStamp = DateTime.Now,
                        Type = 1,
                        UserPublicAddress = appUser.ExternalUserPublicAddress
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                }
                else
                {
                    var applicationEvent = new ApplicationSideEvent()
                    {
                        Message = response,
                        TimeStamp = DateTime.Now,
                        Type = 0,
                        UserPublicAddress = appUser.ExternalUserPublicAddress
                    };
                    await _blockchainEntityFrameworkService.AddEvent(applicationEvent);
                    return View(IndexAsync());
                }
                /*add event in any case!*/
                return View();
                //return await IndexAsync();
            }
            catch (Exception)
            {
                //return await IndexAsync();
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
