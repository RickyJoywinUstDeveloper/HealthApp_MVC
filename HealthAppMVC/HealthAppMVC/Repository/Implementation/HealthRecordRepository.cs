using HealthAppMVC.Models;
using HealthAppMVC.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HealthAppMVC.Repository.Implementation
{
    public class HealthRecordRepository
          : IHealthRecordRepository
    {
        private readonly string _connectionString;

        public HealthRecordRepository()
        {
            _connectionString =
                ConfigurationManager
                .ConnectionStrings["HealthDb"]
                .ConnectionString;
        }

        public void Add(
            HealthRecord record)
        {
            using (SqlConnection con =
                new SqlConnection(_connectionString))
            {
                string query =
                @"INSERT INTO HealthRecords
                (
                    AppointmentId,
                    VisitDate,
                    Diagnosis,
                    Prescription,
                    Notes
                )
                VALUES
                (
                   
                    @AppointmentId,
                    @VisitDate,
                    @Diagnosis,
                    @Prescription,
                    @Notes
                )";

                SqlCommand cmd =
                    new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@AppointmentId",
                    record.AppointmentId);

                cmd.Parameters.AddWithValue(
                    "@VisitDate",
                    record.VisitDate);

                cmd.Parameters.AddWithValue(
                    "@Diagnosis",
                    record.Diagnosis);

                cmd.Parameters.AddWithValue(
                    "@Prescription",
                    record.Prescription);

                cmd.Parameters.AddWithValue(
                    "@Notes",
                    string.IsNullOrWhiteSpace(
                        record.Notes)
                    ? (object)DBNull.Value
                    : record.Notes);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public HealthRecord GetById(
            int recordId)
        {
            HealthRecord record = null;

            using (SqlConnection con =
                new SqlConnection(_connectionString))
            {
                string query =
                @"SELECT
                  hr.*,
                  p.PatientId,
                  p.FullName AS PatientName,
                  d.FullName AS DoctorName,
                  d.Specialisation
                  FROM HealthRecords hr
                  INNER JOIN Appointments a
                  ON hr.AppointmentId = a.AppointmentId
                  INNER JOIN Patients p
                  ON a.PatientId = p.PatientId
                  INNER JOIN Doctors d
                  ON a.DoctorId = d.DoctorId
                  WHERE hr.HealthRecordId=@RecordId";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@RecordId",
                    recordId);

                con.Open();

                SqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    record = new HealthRecord
                    {
                        HealthRecordId =
                           Convert.ToInt32
                           (reader["HealthRecordId"]),

                        PatientId =
                          Convert.ToInt32(
                          reader["PatientId"]),


                        AppointmentId =
                            Convert.ToInt32(
                                reader["AppointmentId"]),

                        VisitDate =
                            Convert.ToDateTime(
                                reader["VisitDate"]),

                        Diagnosis =
                            reader["Diagnosis"]
                            .ToString(),

                        Prescription =
                            reader["Prescription"]
                            .ToString(),

                        Notes =
                            reader["Notes"] ==
                            DBNull.Value
                            ? ""
                            : reader["Notes"]
                            .ToString(),

                        PatientName =
                            reader["PatientName"]
                            .ToString(),

                        DoctorName =
                            reader["DoctorName"]
                            .ToString(),
                        Specialisation =
                         ((SpecialisationType)
                          Convert.ToInt32(reader["Specialisation"]))
                          .ToString()
                    };
                }
            }

            return record;
        }

        public List<HealthRecord> GetByPatientId(
            int patientId)
        {
            List<HealthRecord> records =
                new List<HealthRecord>();

            using (SqlConnection con =
                new SqlConnection(_connectionString))
            {
                string query =
                @"SELECT
    hr.*,

    p.FullName AS PatientName,
    d.FullName AS DoctorName,
    d.Specialisation
FROM HealthRecords hr
INNER JOIN Appointments a
    ON hr.AppointmentId = a.AppointmentId
INNER JOIN Patients p
    ON a.PatientId = p.PatientId
INNER JOIN Doctors d
    ON a.DoctorId = d.DoctorId
WHERE a.PatientId=@PatientId
ORDER BY hr.VisitDate DESC";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@PatientId",
                    patientId);

                con.Open();

                SqlDataReader reader =
                    cmd.ExecuteReader();

                while (reader.Read())
                {
                    records.Add(
                        new HealthRecord
                        {
                            HealthRecordId =
                                Convert.ToInt32(
                                    reader["HealthRecordId"]),

                          
                            AppointmentId =
                                Convert.ToInt32(
                                    reader["AppointmentId"]),

                            VisitDate =
                                Convert.ToDateTime(
                                    reader["VisitDate"]),

                            Diagnosis =
                                reader["Diagnosis"]
                                .ToString(),

                            Prescription =
                                reader["Prescription"]
                                .ToString(),

                            Notes =
                                reader["Notes"] ==
                                DBNull.Value
                                ? ""
                                : reader["Notes"]
                                .ToString(),

                            PatientName =
                                reader["PatientName"]
                                .ToString(),

                            DoctorName =
                                reader["DoctorName"]
                                .ToString(),

                            Specialisation =
                            ((SpecialisationType)
                            Convert.ToInt32(reader["Specialisation"]))
                           .ToString()
                        });
                }
            }

            return records;
        }
    }
}