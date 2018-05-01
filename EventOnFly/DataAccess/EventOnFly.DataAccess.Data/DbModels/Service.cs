using EventOnFly.Common.Interfaces.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventOnFly.DataAccess.Data.DbModels
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ServiceState State { get; set; }

        public ServiceType ServiceType { get; set; }

        public int UserId { get; set; }
    }
}
