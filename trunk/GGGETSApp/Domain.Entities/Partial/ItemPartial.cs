﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    //[Serializable]
    public partial class Item
    {
        public Item()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    }
}