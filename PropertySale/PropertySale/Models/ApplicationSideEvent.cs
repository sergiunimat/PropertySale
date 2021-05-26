using Ethereum.Entity.Framework.FrameworkDataAnnotations.EventAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySale.Models
{
    [FrameworkEvent]
    public class ApplicationSideEvent
    {
        [FrameworkEventIdInt]
        public int Id { get; set; }
        [FrameworkEventTimeStampDateTime]
        public DateTime TimeStamp { get; set; }
        [FrameworkEventTypeInt]
        public int Type { get; set; }
        [FrameworkEventMessageString]
        public string Message { get; set; }
        [FrameworkEventUserPublicAddressString]
        public string UserPublicAddress { get; set; }   //who triggered it
    }
}
