using System;
using System.Collections.Generic;
using System.Text;

namespace EventOnFly.DataAccess.Data.DbModels
{
    /// <summary>
    /// Vendor user entity
    /// </summary>
    public class Vendor
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }
    }
}
