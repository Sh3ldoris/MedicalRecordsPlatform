using System.Collections;
using MedicalRecords.Model;

namespace MedicalRecords.Service;

public interface IDoctorService
{
    Task<IEnumerable> GetAllDoctorsAsync();
    Task<Doctor?> GetDoctorByIdAsync(int id);
    Task<Doctor> CreateDoctorAsync(Doctor doctor);
    Task<bool> UpdateDoctorAsync(int id, Doctor doctor);
    Task<bool> DeleteDoctorAsync(int id);
    Task<bool> DoctorExistsAsync(int id);
}