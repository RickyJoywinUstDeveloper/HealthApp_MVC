using HealthAppWebApi.App_Data;
using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebApi.Repositories.Impl
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            return _context.Doctors.Find(id);
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            _context.Entry(doctor).State =
                System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }

        public void ChangeStatus(int id, bool isActive)
        {
            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
                return;

            doctor.IsActive = isActive;

            _context.SaveChanges();
        }

        public List<Doctor> GetBySpecialisation(
    SpecialisationType specialisation)
        {
            return _context.Doctors
                .Where(d => d.Specialisation == specialisation)
                .ToList();
        }
    }
}