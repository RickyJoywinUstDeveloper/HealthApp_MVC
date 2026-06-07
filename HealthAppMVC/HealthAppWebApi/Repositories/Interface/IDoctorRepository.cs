using HealthAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Repositories.Interface
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAll();

        Doctor GetById(int id);

        void Add(Doctor doctor);

        void Update(Doctor doctor);

        void ChangeStatus(int id, bool isActive);

        List<Doctor> GetBySpecialisation(
    SpecialisationType specialisation);
    }
}
