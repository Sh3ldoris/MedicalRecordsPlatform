using System.Collections;
using MedicalRecords.Model;

namespace MedicalRecords.Service;

public interface IPersonService
{
    Task<IEnumerable> GetAllPersonsAsync();
    Task<Person?> GetPersonByIdAsync(int id);
    Task<Person> CreatePersonAsync(Person person);
    Task<bool> UpdatePersonAsync(int id, Person person);
    Task<bool> DeletePersonAsync(int id);
    Task<bool> PersonExistsAsync(int id);
}