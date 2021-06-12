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
            //await _smartContractService.DeployPropertySaleContractAsync("8f5370e51350f2c3b2a34a79c9b7e4f5d6899a02ae7db3d47feadee532073c38");
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
                        Message = "The property was successfully transfered from: " + appUserSeller.ExternalUserPublicAddress + " to: " + appUserBuyer.ExternalUserPublicAddress,
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
