using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthAppMVC.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public SpecialisationType Specialisation { get; set; }

        [Required]
        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "100000")]
        public decimal ConsultationFee { get; set; }

        public bool IsActive { get; set; }
    }
}