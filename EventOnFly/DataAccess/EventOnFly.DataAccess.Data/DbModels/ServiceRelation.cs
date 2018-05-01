using EventOnFly.Common.Interfaces.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventOnFly.DataAccess.Data.DbModels
{
    public class ServiceRelation
    {
        [Key]
        public int Service1Id { get; set; }

        public Service Service1 { get; set; }

        [Key]
        public int Service2Id { get; set; }

        public Service Service2 { get; set; }

        public ServiceRelationType RelationType { get; set; }
    }
}
