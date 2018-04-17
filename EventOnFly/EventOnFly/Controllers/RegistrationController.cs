using System.Threading.Tasks;
using EventOnFly.Enums;
using EventOnFly.VendorSide.BusinessLogic;
using EventOnFly.VendorSide.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EventOnFly.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        [HttpPost("[action]")]
        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            return await registrationService.StartRegistration(form);
        }
    }
}
