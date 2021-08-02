using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekDAL.Models
{
    public class MeterReading
    {
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public DateTime Taken { get; set; }
        [Required]
        public int Reading { get; set; }
        [NotMapped]
        public bool IsValid { get; set; }
    }
}
