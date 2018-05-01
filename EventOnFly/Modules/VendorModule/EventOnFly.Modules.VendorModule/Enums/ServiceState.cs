using System;
using System.Collections.Generic;
using System.Text;

namespace EventOnFly.Modules.VendorModule.Enums
{
    public enum ServiceState
    {
        Valid = 1,
        InRegistrationProcess = 2,
        AwaitingValidation = 4,
        Invalid = 8
    }
}
