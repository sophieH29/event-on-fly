using System.Collections.Generic;
using EventOnFly.Enums;

namespace EventOnFly.VendorSide.Dtos
{
    public class StartRegistrationForm
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public List<ServiceType> AttachedServiceTypes { get; set; }
    }
}
