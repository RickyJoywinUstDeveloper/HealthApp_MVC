using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthAppWebApi.Models
{
    public enum SpecialisationType
    {
        Cardiologist,
        Dermatologist,
        Neurologist,
        Orthopedic,
        Pediatrician,
        GeneralPhysician
    }

    public class Doctor
    {
        [Key]
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
        public decimal ConsultationFee { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(
           @"^[0-9]{10}$",
           ErrorMessage = "Phone number must contain exactly 10 digits")]
        public string DoctorPhoneNo { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(
            ErrorMessage = "Invalid email format")]
        [StringLength(100)]
        public string DoctorEmail { get; set; }
    }
}