using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthAppWebApi.Models
{
    public class HealthRecord
    {
        [Key]
        public int HealthRecordId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public string Prescription { get; set; }

        public string Notes { get; set; }

        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}