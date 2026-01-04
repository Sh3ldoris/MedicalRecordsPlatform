using System.Collections;
using Microsoft.AspNetCore.Mvc;
using MedicalRecords.Model;
using MedicalRecords.Service;

namespace MedicalRecords.Controller;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    // GET: api/Persons
    [HttpGet]
    public async Task<ActionResult<IEnumerable>> GetPersons()
    {
        var persons = await _personService.GetAllPersonsAsync();
        return Ok(persons);
    }

    // GET: api/Persons/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetPerson(int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    // POST: api/Persons
    [HttpPost]
    public async Task<ActionResult> PostPerson(Person person)
    {
        var createdPerson = await _personService.CreatePersonAsync(person);
        return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
    }

    // PUT: api/Persons/5
    [HttpPut("{id}")]
    public async Task<StatusCodeResult> PutPerson(int id, Person person)
    {
        var result = await _personService.UpdatePersonAsync(id, person);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Persons/5
    [HttpDelete("{id}")]
    public async Task<StatusCodeResult> DeletePerson(int id)
    {
        var result = await _personService.DeletePersonAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}