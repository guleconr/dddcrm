using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBBProject.Admin.Web
{

    public enum AppointmentStatus
    {
        AppointmentSet = 1,
        Done = 2,
        NotCalled = 3,
        CouldNotSet = 4,
        DidNotCome = 5,
        CancelledAuto = 6
    }

}
