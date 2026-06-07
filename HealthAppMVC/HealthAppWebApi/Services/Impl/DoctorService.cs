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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public List<DoctorDto> GetAllDoctors()
        {
            return _repo.GetAll()
                .Select(d => new DoctorDto
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName,
                    Specialisation =
                        d.Specialisation.ToString(),
                    ConsultationFee =
                        d.ConsultationFee,
                    IsActive = d.IsActive,
                    DoctorPhoneNo = d.DoctorPhoneNo
                })
                .ToList();
        }

        public DoctorDto GetDoctorById(int id)
        {
            var doctor = _repo.GetById(id);

            if (doctor == null)
                return null;

            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Specialisation =
                    doctor.Specialisation.ToString(),
                ConsultationFee =
                    doctor.ConsultationFee,
                IsActive = doctor.IsActive,
                DoctorPhoneNo= doctor.DoctorPhoneNo
            };
        }

        public void AddDoctor(CreateDoctorDto dto)
        {

            if (!Enum.TryParse(
            dto.Specialisation,
            true,
            out SpecialisationType specialisation))
            {
                throw new Exception(
                    "Invalid Specialisation.");
            }

            Doctor doctor = new Doctor
            {
                FullName = dto.FullName,

                Specialisation = specialisation,
                    
                YearsOfExperience =
                    dto.YearsOfExperience,

                ConsultationFee =
                    dto.ConsultationFee,

                DoctorEmail = dto.DoctorEmail,

                DoctorPhoneNo = dto.DoctorPhoneNo,

                IsActive = true
            };



            _repo.Add(doctor);
        }

        public void UpdateDoctor(
            int id,
            CreateDoctorDto dto)
        {
            Doctor doctor =
                _repo.GetById(id);

            if (doctor == null)
            {
                throw new Exception(
                    "Doctor not found.");
            }

            if (!Enum.TryParse(
            dto.Specialisation,
            true,
            out SpecialisationType specialisation))
            {
                throw new Exception(
                    "Invalid Specialisation.");
            }


            doctor.FullName =
                dto.FullName;

            doctor.Specialisation = specialisation;

            doctor.YearsOfExperience =
                dto.YearsOfExperience;

            doctor.ConsultationFee =
                dto.ConsultationFee;

            doctor.DoctorEmail = dto.DoctorEmail;

            doctor.DoctorPhoneNo = dto.DoctorPhoneNo;

            _repo.Update(doctor);
        }

        public void ChangeStatus(
            int id,
            bool isActive)
        {
            _repo.ChangeStatus(
                id,
                isActive);
        }

        public List<DoctorDto> GetDoctorsBySpecialisation(
    string specialisation)
        {
            if (!Enum.TryParse(
                    specialisation,
                    true,
                    out SpecialisationType spec))
            {
                throw new Exception(
                    "Invalid Specialisation.");
            }

            return _repo
                .GetBySpecialisation(spec)
                .Select(d => new DoctorDto
                {
                    DoctorId = d.DoctorId,
                    FullName = d.FullName,
                    Specialisation =
                        d.Specialisation.ToString(),
                    ConsultationFee =
                        d.ConsultationFee,
                    IsActive = d.IsActive,
                    DoctorPhoneNo =
                        d.DoctorPhoneNo
                })
                .ToList();
        }
    }
}