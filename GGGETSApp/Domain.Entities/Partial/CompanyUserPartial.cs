﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    public partial class CompanyUser
    {
        public CompanyUser()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    }
}
