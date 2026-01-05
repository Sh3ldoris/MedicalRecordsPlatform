using Refit;

namespace MedicalRecordsUI.Services;

public interface IMedicalRecordsApi
{
    // Person endpoints
    [Get("/api/Persons")]
    Task<List<Person>> GetPersonsAsync();

    [Get("/api/Persons/{id}")]
    Task<Person> GetPersonAsync(int id);

    [Post("/api/Persons")]
    Task CreatePersonAsync([Body] Person person);

    [Put("/api/Persons/{id}")]
    Task UpdatePersonAsync(int id, [Body] Person person);

    [Delete("/api/Persons/{id}")]
    Task DeletePersonAsync(int id);

    // Doctor endpoints
    [Get("/api/Doctors")]
    Task<List<Doctor>> GetDoctorsAsync();

    [Get("/api/Doctors/{id}")]
    Task<Doctor> GetDoctorAsync(int id);

    [Post("/api/Doctors")]
    Task CreateDoctorAsync([Body] Doctor doctor);

    [Put("/api/Doctors/{id}")]
    Task UpdateDoctorAsync(int id, [Body] Doctor doctor);

    [Delete("/api/Doctors/{id}")]
    Task DeleteDoctorAsync(int id);

    // MedicalRecord endpoints
    [Get("/api/MedicalRecords")]
    Task<List<MedicalRecord>> GetMedicalRecordsAsync();

    [Get("/api/MedicalRecords/{id}")]
    Task<MedicalRecord> GetMedicalRecordAsync(int id);

    [Post("/api/MedicalRecords")]
    Task CreateMedicalRecordAsync([Body] MedicalRecord record);

    [Put("/api/MedicalRecords/{id}")]
    Task UpdateMedicalRecordAsync(int id, [Body] MedicalRecord record);

    [Delete("/api/MedicalRecords/{id}")]
    Task DeleteMedicalRecordAsync(int id);
}
