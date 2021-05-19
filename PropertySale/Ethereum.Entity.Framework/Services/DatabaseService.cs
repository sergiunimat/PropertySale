﻿using Ethereum.Entity.Framework.Data;
using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nethereum.JsonRpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Services
{
    public class DatabaseService:IDatabaseService
    {
        readonly ApplicationDbContext _ctx;

        public DatabaseService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        /*Service-class resposible for database operations*/

        #region Users related services
        public async Task AddUserAsync(User user) {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
        }
        public async Task<User> GetUserByPublicAddressAsync(string publicAddress) {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.PublicAddress == publicAddress);
        }

        public async Task<List<User>> GetAllUsersAsync() {
            return await _ctx.Users.ToListAsync();
        }

        public async Task UpdateUserEtherBalanceAsync(string publicAddress,string ether) {
            try
            {
                var usertToUpdate = await GetUserByPublicAddressAsync(publicAddress);
                usertToUpdate.Ether = ether;
                await _ctx.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region Property related services
        public async Task AddPropertyAsync(Property property)
        {
            await _ctx.Properties.AddAsync(property);
            await _ctx.SaveChangesAsync();
        }

        public async Task EditPropertyAsync(Property property)
        {
            var propertyToEdit = await _ctx.Properties.FirstOrDefaultAsync(p => p.Id == property.Id);
            propertyToEdit = property;
            await _ctx.SaveChangesAsync();
        }

        public async Task RemovePropertyAsync(string propertyId) {
            try
            {
                var propertyToDelete = await GetpropertyByPropertyIdAsync(propertyId);
                _ctx.Properties.Remove(propertyToDelete);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task<Property> GetpropertyByPropertyIdAsync(string propertyId)
        {
            return await _ctx.Properties.FirstOrDefaultAsync(p => p.Id== propertyId);
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _ctx.Properties.ToListAsync();
        }

        #endregion

        #region Event related services
        public async Task AddEventAsync(Event incommingEvent) {
            await _ctx.Events.AddAsync(incommingEvent);
        }

        public async Task<List<Event>> GetAllEventsAsync() {
           return await _ctx.Events.ToListAsync();
        }
        #endregion
    }
}
