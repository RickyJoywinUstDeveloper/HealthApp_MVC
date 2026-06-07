using HealthAppWebApi.App_Data;
using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace HealthAppWebApi.Repositories.Impl
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;


        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return _context.Patients.Find(id);
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public void Update(Patient patient)
        {
            _context.Entry(patient).State =
                System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }
    }
}