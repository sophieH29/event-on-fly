using EventOnFly.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventOnFly.Data.DbModels
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
