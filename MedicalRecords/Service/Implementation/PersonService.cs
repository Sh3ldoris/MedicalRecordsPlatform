using System.Collections;
using MedicalRecords.Data;
using MedicalRecords.Model;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Service.Implementation;

public class PersonService: IPersonService
{
    private readonly ApplicationDbContext _context;

    public PersonService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable> GetAllPersonsAsync()
    {
        return await _context.Persons
            .Include(p => p.Doctor)
            .Include(p => p.MedicalRecords)
            .ToListAsync();
    }

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        return await _context.Persons
            .Include(p => p.Doctor)
            .Include(p => p.MedicalRecords)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<bool> UpdatePersonAsync(int id, Person person)
    {
        if (id != person.Id)
        {
            return false;
        }

        _context.Entry(person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await PersonExistsAsync(id))
            {
                return false;
            }
            throw;
        }
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null)
        {
            return false;
        }

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> PersonExistsAsync(int id)
    {
        return await _context.Persons.AnyAsync(e => e.Id == id);
    }
}