using System.Threading.Tasks;
using EventOnFly.BusinessLogic;
using EventOnFly.Dtos;
using EventOnFly.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EventOnFly.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        public async Task<StartRegistrationResult> StartRegistration(StartRegistrationForm form)
        {
            return await registrationService.StartRegistration(form);
        }
    }
}
