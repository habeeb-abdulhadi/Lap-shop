﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class ApplicationRole : IdentityRole
    {
        public string RolePermisstions { get; set; }
    }
}
