﻿using IntelliViews.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliViews.Data.DataModels
{
    public class ApplicationUser : IEntity
    {
        public string Id { get; }
        public required Roles Role { get; set; }
    }
}
