using System;
using Microsoft.AspNetCore.Identity;

namespace EventOnFly.DataAccess.Data.DbModels
{
    /// <summary>
    /// Application User
    /// </summary>
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public long? GoogleId { get; set; }
        public string PictureUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
