using EventOnFly.Modules.VendorModule.Enums;
using System.Collections.Generic;

namespace EventOnFly.Modules.VendorModule.Dtos
{
    public class StartRegistrationForm
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public List<ServiceType> AttachedServiceTypes { get; set; }
    }
}
