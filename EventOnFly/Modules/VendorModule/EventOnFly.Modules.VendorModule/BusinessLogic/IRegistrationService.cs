using EventOnFly.Common.Interfaces.Enums;
using EventOnFly.Modules.VendorModule.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventOnFly.Modules.VendorModule.BusinessLogic
{
    public interface IRegistrationService
    {
        Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form);
    }
}
