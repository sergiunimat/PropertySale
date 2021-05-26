﻿using Ethereum.Entity.Framework.FrameworkDataAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.EstatePropertyAnnotations;
using Ethereum.Entity.Framework.FrameworkDataAnnotations.EventAnnotations;
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
    public class ReflextionService: IReflextionService
    {
        /*create the reflection services*/
        public UserPropertyDTO BuildInternalDTO<T,I>(T estatePropertyItem, I frameworkUserItem) where T : new() where I:new()
        {
            var userObj = new User();
            var propertyObj = new Property();
            try
            {
                if (estatePropertyItem == null && frameworkUserItem == null)
                    return new UserPropertyDTO() { ErrorMessage = "Incoming paramters are set to null" };

                /*handle PROPERTY*/
                if (estatePropertyItem != null)
                {                    
                    if (estatePropertyItem.GetType().GetCustomAttributesData().Count()<=0 || estatePropertyItem.GetType().GetCustomAttributesData()[0].AttributeType.Name != typeof(EstateProperty).Name)
                        return new UserPropertyDTO() { ErrorMessage = "The external estate propety objec is not decorated with \"[EstateProperty]\" data annotation." };

                    var externalObjectProperties = estatePropertyItem.GetType().GetProperties();
                    foreach (var externalProperty in externalObjectProperties)
                    {
                        var attributes = externalProperty.GetCustomAttributes(false);
                        foreach (var attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(EstatePropertyIdString))
                                propertyObj.Id = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropertyDescriptionString))
                                propertyObj.Description = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropertyEtherString))
                                propertyObj.Ether = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropertyOwnerPublicAddressString))
                                propertyObj.OwnerPublicAddress = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropetyGeographicalAddressString))
                                propertyObj.GeographicalAddress = externalProperty.GetValue(estatePropertyItem).ToString();
                        }
                    }
                }

                /*Handle framework user*/
                if (frameworkUserItem != null)
                {

                    if (frameworkUserItem.GetType().GetCustomAttributesData().Count()<=0||frameworkUserItem.GetType().GetCustomAttributesData()[0].AttributeType.Name != typeof(FrameworkUser).Name)
                        return new UserPropertyDTO() { ErrorMessage = "The external user object is not decorated with \"[FrameworkUser]\" data annotation." };

                    var externalObjectProperties = frameworkUserItem.GetType().GetProperties();
                    foreach (var externalFrameworkUser in externalObjectProperties)
                    {
                        var attributes = externalFrameworkUser.GetCustomAttributes(false);
                        foreach (var attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(FrameworkUserEmailString))
                                userObj.Email = externalFrameworkUser.GetValue(frameworkUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserEtherString))
                                userObj.Ether = externalFrameworkUser.GetValue(frameworkUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserFullNameString))
                                userObj.FullName = externalFrameworkUser.GetValue(frameworkUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserIdInt))
                                userObj.Id = (int)externalFrameworkUser.GetValue(frameworkUserItem);
                            if (attribute.GetType() == typeof(FrameworkUserPrivateAddressString))
                                userObj.PrivateAddress = externalFrameworkUser.GetValue(frameworkUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserPublicAddressString))
                                userObj.PublicAddress = externalFrameworkUser.GetValue(frameworkUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserTypeInt))
                                userObj.Type = (int)externalFrameworkUser.GetValue(frameworkUserItem);
                        }
                    }
                }

                return new UserPropertyDTO() { User = userObj, Property = propertyObj, ErrorMessage = ResponseStatus.SUCCESS };

            }
            catch (Exception e)
            {
                return new UserPropertyDTO() { ErrorMessage = e.Message };
                throw;
            }

        }

        public TransferPropertyDTO BuildInternalTransferPropertyDTO<T,I,Z>(T estatePropertyItem, I frameworkSellerUserItem, Z frameworkBuyerUserItem) where T : new() where I : new() where Z : new()
        {

            var seller = new User();
            var buyer = new User();
            var propertyObj = new Property();
            try
            {
                if (estatePropertyItem == null && frameworkSellerUserItem == null && frameworkBuyerUserItem == null)
                    return new TransferPropertyDTO() { ErrorMessage = "Incoming paramters are set to null" };

                /*handle PROPERTY*/
                if (estatePropertyItem != null)
                {
                    if (estatePropertyItem.GetType().GetCustomAttributesData().Count() <= 0 || estatePropertyItem.GetType().GetCustomAttributesData()[0].AttributeType.Name != typeof(EstateProperty).Name)
                        return new TransferPropertyDTO() { ErrorMessage = "The external estate propety objec is not decorated with \"[EstateProperty]\" data annotation." };

                    var externalObjectProperties = estatePropertyItem.GetType().GetProperties();
                    foreach (var externalProperty in externalObjectProperties)
                    {
                        var attributes = externalProperty.GetCustomAttributes(false);
                        foreach (var attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(EstatePropertyIdString))
                                propertyObj.Id = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropertyDescriptionString))
                                propertyObj.Description = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropertyEtherString))
                                propertyObj.Ether = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropertyOwnerPublicAddressString))
                                propertyObj.OwnerPublicAddress = externalProperty.GetValue(estatePropertyItem).ToString();
                            if (attribute.GetType() == typeof(EstatePropetyGeographicalAddressString))
                                propertyObj.GeographicalAddress = externalProperty.GetValue(estatePropertyItem).ToString();
                        }
                    }
                }

                
                if (frameworkSellerUserItem != null)
                {

                    if (frameworkSellerUserItem.GetType().GetCustomAttributesData().Count() <= 0 || frameworkSellerUserItem.GetType().GetCustomAttributesData()[0].AttributeType.Name != typeof(FrameworkUser).Name)
                        return new TransferPropertyDTO() { ErrorMessage = "The external user object is not decorated with \"[FrameworkUser]\" data annotation." };

                    var externalObjectProperties = frameworkSellerUserItem.GetType().GetProperties();
                    foreach (var externalFrameworkUser in externalObjectProperties)
                    {
                        var attributes = externalFrameworkUser.GetCustomAttributes(false);
                        foreach (var attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(FrameworkUserEmailString))
                                seller.Email = externalFrameworkUser.GetValue(frameworkSellerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserEtherString))
                                seller.Ether = externalFrameworkUser.GetValue(frameworkSellerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserFullNameString))
                                seller.FullName = externalFrameworkUser.GetValue(frameworkSellerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserIdInt))
                                seller.Id = (int)externalFrameworkUser.GetValue(frameworkSellerUserItem);
                            if (attribute.GetType() == typeof(FrameworkUserPrivateAddressString))
                                seller.PrivateAddress = externalFrameworkUser.GetValue(frameworkSellerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserPublicAddressString))
                                seller.PublicAddress = externalFrameworkUser.GetValue(frameworkSellerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserTypeInt))
                                seller.Type = (int)externalFrameworkUser.GetValue(frameworkSellerUserItem);
                        }
                    }
                }

                if (frameworkBuyerUserItem != null)
                {

                    if (frameworkBuyerUserItem.GetType().GetCustomAttributesData().Count() <= 0 || frameworkBuyerUserItem.GetType().GetCustomAttributesData()[0].AttributeType.Name != typeof(FrameworkUser).Name)
                        return new TransferPropertyDTO() { ErrorMessage = "The external user object is not decorated with \"[FrameworkUser]\" data annotation." };

                    var externalObjectProperties = frameworkBuyerUserItem.GetType().GetProperties();
                    foreach (var externalFrameworkUser in externalObjectProperties)
                    {
                        var attributes = externalFrameworkUser.GetCustomAttributes(false);
                        foreach (var attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(FrameworkUserEmailString))
                                buyer.Email = externalFrameworkUser.GetValue(frameworkBuyerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserEtherString))
                                buyer.Ether = externalFrameworkUser.GetValue(frameworkBuyerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserFullNameString))
                                buyer.FullName = externalFrameworkUser.GetValue(frameworkBuyerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserIdInt))
                                buyer.Id = (int)externalFrameworkUser.GetValue(frameworkBuyerUserItem);
                            if (attribute.GetType() == typeof(FrameworkUserPrivateAddressString))
                                buyer.PrivateAddress = externalFrameworkUser.GetValue(frameworkBuyerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserPublicAddressString))
                                buyer.PublicAddress = externalFrameworkUser.GetValue(frameworkBuyerUserItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkUserTypeInt))
                                buyer.Type = (int)externalFrameworkUser.GetValue(frameworkBuyerUserItem);
                        }
                    }
                }

                return new TransferPropertyDTO() { Seller = seller, Buyer = buyer, Property = propertyObj, ErrorMessage = ResponseStatus.SUCCESS };

            }
            catch (Exception e)
            {
                return new TransferPropertyDTO() { ErrorMessage = e.Message };
                throw;
            }
        }

        public EventDTO BuildInternalEventDTO<T>(T eventItem) where T : new()
        {
            var eventObj = new Event();
            
            try
            {
                if (eventItem == null)
                    return new EventDTO() { ErrorMessage = "Incoming paramters are set to null while converting to internal EventDTO"};
                
                if (eventItem != null)
                {
                    if (eventItem.GetType().GetCustomAttributesData().Count() <= 0 || eventItem.GetType().GetCustomAttributesData()[0].AttributeType.Name != typeof(FrameworkEvent).Name)
                        return new EventDTO() { ErrorMessage = "The external event objec is not decorated with \"[FrameworkEvent]\" data annotation." };

                    var externalObjectProperties = eventItem.GetType().GetProperties();
                    foreach (var externalEvent in externalObjectProperties)
                    {
                        var attributes = externalEvent.GetCustomAttributes(false);
                        foreach (var attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(FrameworkEventIdInt))
                                eventObj.Id = (int) externalEvent.GetValue(eventItem);
                            if (attribute.GetType() == typeof(FrameworkEventMessageString))
                                eventObj.Message = externalEvent.GetValue(eventItem).ToString();
                            if (attribute.GetType() == typeof(FrameworkEventTimeStampDateTime))
                                eventObj.TimeStamp = (DateTime) externalEvent.GetValue(eventItem);
                            if (attribute.GetType() == typeof(FrameworkEventTypeInt))
                                eventObj.Type = (int) externalEvent.GetValue(eventItem);
                            if (attribute.GetType() == typeof(FrameworkEventUserPublicAddressString))
                                eventObj.UserPublicAddress = externalEvent.GetValue(eventItem).ToString();
                        }
                    }
                }                            
                return new EventDTO() { Event=eventObj, ErrorMessage = ResponseStatus.SUCCESS };
            }
            catch (Exception e)
            {
                return new EventDTO() { ErrorMessage = e.Message };
                throw;
            }

        }
    }
}
