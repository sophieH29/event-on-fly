using System.ComponentModel.DataAnnotations;

namespace EventOnFly.DataAccess.Data.DbModels
{
    public class ServiceOrderPropertyValue
    {
        [Key]
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        [Key]
        public int ServiceId { get; set; }

        [Key]
        public int BookingId { get; set; }

        public ServiceOrder ServiceOrder { get; set; }

        public int? PropertyValueId { get; set; }

        public PropertyValue PropertyValue { get; set; }
    }
}
