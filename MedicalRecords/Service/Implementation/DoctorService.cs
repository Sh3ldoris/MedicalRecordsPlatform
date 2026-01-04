using System.Collections;
using MedicalRecords.Data;
using MedicalRecords.Model;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Service.Implementation;

public class DoctorService: IDoctorService
{
    private readonly ApplicationDbContext _context;

    public DoctorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable> GetAllDoctorsAsync()
    {
        return await _context.Doctors
            .Include(d => d.Person)
            .Include(d => d.AssignedMedicalRecords)
            .ToListAsync();
    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        return await _context.Doctors
            .Include(d => d.Person)
            .Include(d => d.AssignedMedicalRecords)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<bool> UpdateDoctorAsync(int id, Doctor doctor)
    {
        if (id != doctor.Id)
        {
            return false;
        }

        _context.Entry(doctor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await DoctorExistsAsync(id))
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return false;
        }

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DoctorExistsAsync(int id)
    {
        return await _context.Doctors.AnyAsync(e => e.Id == id);
    }
}