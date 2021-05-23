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

        public User BuildUserObject<T>(T frameworkUser) where T : new()
        {

            return null;
        }
    }
}
