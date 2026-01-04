using System.Collections;
using MedicalRecords.Model;
using MedicalRecords.Service;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRecords.Controller;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    // GET: api/Doctors
    [HttpGet]
    public async Task<ActionResult<IEnumerable>> GetDoctors()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        return Ok(doctors);
    }

    // GET: api/Doctors/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetDoctor(int id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }

    // POST: api/Doctors
    [HttpPost]
    public async Task<ActionResult> PostDoctor(Doctor doctor)
    {
        var createdDoctor = await _doctorService.CreateDoctorAsync(doctor);
        return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.Id }, createdDoctor);
    }

    // PUT: api/Doctors/5
    [HttpPut("{id}")]
    public async Task<StatusCodeResult> PutDoctor(int id, Doctor doctor)
    {
        var result = await _doctorService.UpdateDoctorAsync(id, doctor);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Doctors/5
    [HttpDelete("{id}")]
    public async Task<StatusCodeResult> DeleteDoctor(int id)
    {
        var result = await _doctorService.DeleteDoctorAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}