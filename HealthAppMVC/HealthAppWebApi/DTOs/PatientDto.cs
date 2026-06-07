using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebApi.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }
    }
}