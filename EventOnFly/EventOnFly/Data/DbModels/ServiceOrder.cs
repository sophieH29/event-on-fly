using System.ComponentModel.DataAnnotations;

namespace EventOnFly.Data.DbModels
{
    public class ServiceOrder
    {
        [Key]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        [Key]
        public int BookingId { get; set; }

        public Booking Booking { get; set; }
    }
}
