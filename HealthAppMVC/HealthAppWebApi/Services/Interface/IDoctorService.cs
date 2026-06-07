using HealthAppWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Services.Interface
{
    public interface IDoctorService
    {
        List<DoctorDto> GetAllDoctors();

        DoctorDto GetDoctorById(int id);

        void AddDoctor(CreateDoctorDto dto);

        void UpdateDoctor(int id,
                          CreateDoctorDto dto);

        void ChangeStatus(int id,
                          bool isActive);

        List<DoctorDto> GetDoctorsBySpecialisation(
    string specialisation);
    }
}
