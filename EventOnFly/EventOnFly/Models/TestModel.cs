using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventOnFly.Models
{
    public class TestModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateTimeColumn { get; set; }

        public string StringColumn { get; set; }
    }
}
