using System.ComponentModel.DataAnnotations;

namespace EventOnFly.DataAccess.Data.DbModels
{
    public class ServicePropertyValue
    {
        [Key]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        [Key]
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public int? PropertyValueId { get; set; }

        public PropertyValue PropertyValue { get; set; }

        public string EvaluationScript { get; set; }
    }
}
