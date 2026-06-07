using HealthAppWebApi.DTOs;
using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using HealthAppWebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebApi.Services.Impl
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public List<PatientDto> GetAllPatients()
        {
            return _repo.GetAll()
                .Select(p => new PatientDto
                {
                    PatientId = p.PatientId,
                    FullName = p.FullName,
                    Gender = p.Gender.ToString(),
                    Email = p.Email
                })
                .ToList();
        }

        public PatientDto GetPatientById(int id)
        {
            var patient = _repo.GetById(id);

            if (patient == null)
                return null;

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FullName = patient.FullName,
                Gender = patient.Gender.ToString(),
                Email = patient.Email
            };
        }

        public void RegisterPatient(CreatePatientDto dto)
        {
            if (dto.DateOfBirth > DateTime.Today)
                throw new Exception("Future date is not allowed.");

            Patient patient = new Patient
            {
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = (GenderType)Enum.Parse(
                    typeof(GenderType),
                    dto.Gender,true),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedDate = DateTime.Now
            };

            _repo.Add(patient);
        }

        public void UpdatePatient(int id, CreatePatientDto dto)
        {
            var patient = _repo.GetById(id);

            if (patient == null)
                throw new Exception("Patient not found.");

            patient.FullName = dto.FullName;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Email = dto.Email;
            patient.PhoneNumber = dto.PhoneNumber;

            patient.Gender =
                (GenderType)Enum.Parse(
                    typeof(GenderType),
                    dto.Gender,true);

            _repo.Update(patient);
        }
    }
}