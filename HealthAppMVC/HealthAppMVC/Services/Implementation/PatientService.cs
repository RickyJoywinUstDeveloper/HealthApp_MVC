using HealthAppMVC.Models;
using HealthAppMVC.Repository.Interface;
using HealthAppMVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppMVC.Services.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(
            IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll();
        }

        public Patient GetPatientById(int id)
        {
            var patient =
                _patientRepository.GetById(id);

            if (patient == null)
            {
                throw new Exception(
                    $"Patient with Id {id} not found.");
            }

            return patient;
        }

        public void RegisterPatient(Patient patient)
        {
            if (_patientRepository.EmailExists(patient.Email))
            {
                throw new Exception(
                    "Email already exists.");
            }

            patient.CreatedDate = DateTime.Now;

            _patientRepository.Add(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            var existingPatient =
                _patientRepository.GetById(
                    patient.PatientId);

            if (existingPatient == null)
            {
                throw new Exception(
                    "Patient not found.");
            }

            _patientRepository.Update(patient);
        }

        public void DeletePatient(int id)
        {
            var patient =
                _patientRepository.GetById(id);

            if (patient == null)
            {
                throw new Exception(
                    "Patient not found.");
            }

            _patientRepository.Delete(id);
        }

        public int GetAppointmentCount(
            int patientId)
        {
            return _patientRepository
                .GetAppointmentCount(patientId);
        }
    }
}