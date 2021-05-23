﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework.Models.DTO
{
    public class UserPropertyDTO
    {
        public User User { get; set; }
        public Property Property { get; set; }
        public string ErrorMessage { get; set; }
    }
}
