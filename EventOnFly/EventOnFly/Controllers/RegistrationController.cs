using System.Threading.Tasks;
using EventOnFly.BusinessLogic;
using EventOnFly.Dtos;
using EventOnFly.Enums;
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
            return StartRegistrationResult.Success;
            //return await registrationService.StartRegistration(form);
        }
    }
}
