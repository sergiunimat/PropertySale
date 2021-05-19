using Ethereum.Entity.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Interfaces
{
    public interface IDatabaseService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByPublicAddressAsync(string publicAddress);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserEtherBalanceAsync(string publicAddress, string ether);
        Task AddPropertyAsync(Property property);
        Task EditPropertyAsync(Property property);
        Task RemovePropertyAsync(string propertyId);
        Task<Property> GetpropertyByPropertyIdAsync(string propertyId);
        Task<List<Property>> GetAllPropertiesAsync();
        Task AddEventAsync(Event incommingEvent);
        Task<List<Event>> GetAllEventsAsync();
    }
}
