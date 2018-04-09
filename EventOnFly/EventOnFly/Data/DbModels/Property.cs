using EventOnFly.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventOnFly.Data.DbModels
{
    public class Property
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public PropertyType PropertyType { get; set; }

        public int? DefaultValueId { get; set; }

        public PropertyValue DefaultVaue { get; set; }
    }
}
