using EventOnFly.Common.Interfaces.Enums;
using EventOnFly.Modules.VendorModule.Dtos;
using System.Threading.Tasks;

namespace EventOnFly.Modules.VendorModule.BusinessLogic
{
    public interface IRegistrationService
    {
        Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form);
    }
}
