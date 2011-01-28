using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    public partial class AddressBook
    {
        public AddressBook()
        {
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    }
}
