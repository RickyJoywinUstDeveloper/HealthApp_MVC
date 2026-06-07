namespace HealthAppWebApi.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }

        public string FullName { get; set; }

        public string Specialisation { get; set; }

        public decimal ConsultationFee { get; set; }

        public bool IsActive { get; set; }

        public string DoctorPhoneNo { get; set; }

    }
}