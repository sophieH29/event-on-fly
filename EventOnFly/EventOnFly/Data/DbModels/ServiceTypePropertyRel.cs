using EventOnFly.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventOnFly.Data.DbModels
{
    public class ServiceTypePropertyRel
    {
        [Key]
        public ServiceType ServiceType { get; set; }

        [Key]
        public int PropertyId { get; set; }

        public Property Property { get; set; }
    }
}
