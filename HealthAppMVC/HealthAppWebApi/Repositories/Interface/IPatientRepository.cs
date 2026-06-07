using HealthAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Repositories.Interface
{
    public interface IPatientRepository
    {
        List<Patient> GetAll();

        Patient GetById(int id);

        void Add(Patient patient);

        void Update(Patient patient);
    }
}
