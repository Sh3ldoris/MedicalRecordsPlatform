using System.Collections;
using MedicalRecords.Data;
using MedicalRecords.Model;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Service.Implementation;

public class MedicalRecordService: IMedicalRecordService
{
    private readonly ApplicationDbContext _context;

        public MedicalRecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable> GetAllMedicalRecordsAsync()
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Person)
                .Include(mr => mr.Doctor)
                .ToListAsync();
        }

        public async Task<MedicalRecord?> GetMedicalRecordByIdAsync(int id)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Person)
                .Include(mr => mr.Doctor)
                .FirstOrDefaultAsync(mr => mr.Id == id);
        }

        public async Task<IEnumerable> GetMedicalRecordsByPersonAsync(int personId)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Doctor)
                .Where(mr => mr.PersonId == personId)
                .ToListAsync();
        }

        public async Task<IEnumerable> GetMedicalRecordsByDoctorAsync(int doctorId)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Person)
                .Where(mr => mr.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
            return medicalRecord;
        }

        public async Task<bool> UpdateMedicalRecordAsync(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id)
            {
                return false;
            }

            _context.Entry(medicalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MedicalRecordExistsAsync(id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteMedicalRecordAsync(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return false;
            }

            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MedicalRecordExistsAsync(int id)
        {
            return await _context.MedicalRecords.AnyAsync(e => e.Id == id);
        }
}