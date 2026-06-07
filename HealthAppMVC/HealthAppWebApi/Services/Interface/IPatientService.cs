using HealthAppWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Services.Interface
{
    public interface IPatientService
    {
        List<PatientDto> GetAllPatients();

        PatientDto GetPatientById(int id);

        void RegisterPatient(CreatePatientDto dto);

        void UpdatePatient(int id, CreatePatientDto dto);
    }
}
