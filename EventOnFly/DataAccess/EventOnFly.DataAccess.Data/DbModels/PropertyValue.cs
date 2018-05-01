using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventOnFly.DataAccess.Data.DbModels
{
    public class PropertyValue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int? IntegerValue { get; set; }

        public string TextValue { get; set; }

        public bool? BooleanValue { get; set; }

        public double? FloatValue { get; set; }
    }
}
