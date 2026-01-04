using System.Collections;
using MedicalRecords.Model;

namespace MedicalRecords.Service;

public interface IMedicalRecordService
{
    Task<IEnumerable> GetAllMedicalRecordsAsync();
    Task<MedicalRecord?> GetMedicalRecordByIdAsync(int id);
    Task<IEnumerable> GetMedicalRecordsByPersonAsync(int personId);
    Task<IEnumerable> GetMedicalRecordsByDoctorAsync(int doctorId);
    Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord);
    Task<bool> UpdateMedicalRecordAsync(int id, MedicalRecord medicalRecord);
    Task<bool> DeleteMedicalRecordAsync(int id);
    Task<bool> MedicalRecordExistsAsync(int id);
}