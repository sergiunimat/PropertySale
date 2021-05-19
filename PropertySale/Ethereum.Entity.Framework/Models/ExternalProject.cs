using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Models
{
    public class ExternalProject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectId { get; set; }
        public string ProjectSecret { get; set; }
        public string ProjectLink { get; set; }
    }
}
