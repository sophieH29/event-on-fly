using EventOnFly.Common.Interfaces.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventOnFly.DataAccess.Data.DbModels
{
    public class ServiceTypeRelation
    {
        [Key]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        [Key]
        public ServiceType ServiceType { get; set; }

        public string AlowanceScript { get; set; }
    }
}
