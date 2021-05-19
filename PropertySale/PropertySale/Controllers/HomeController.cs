using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PropertySale.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseService _databaseService;

        public HomeController(ILogger<HomeController> logger, IDatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            #region Testing database services
            /*Test User*/
            //var testUserI = new User()
            //{
            //    Email="test@mail.com",
            //    Ether="2",
            //    FullName="Test user",
            //    PrivateAddress="PRIVTE ADDRESS STRING",
            //    PublicAddress = "PUBLIC ADDRESS",
            //    Type = 1,
            //};
            //var testUserII = new User()
            //{
            //    Email = "test1@mail.com",
            //    Ether = "20",
            //    FullName = "Test user two",
            //    PrivateAddress = "PRIVTE ADDRESS STRING TWO",
            //    PublicAddress = "PUBLIC ADDRESS TWO",
            //    Type = 0,
            //};

            //await _databaseService.AddUserAsync(testUserI);
            //await _databaseService.AddUserAsync(testUserII);
            //var listOfUsers = await _databaseService.GetAllUsersAsync();
            //var retrievedUser = await _databaseService.GetUserByPublicAddressAsync("PUBLIC ADDRESS TWO");
            //await _databaseService.UpdateUserEtherBalanceAsync("PUBLIC ADDRESS TWO", "66");
            //var listOfUsersII = await _databaseService.GetAllUsersAsync();
            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
