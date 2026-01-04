using System.Collections;
using MedicalRecords.Model;
using MedicalRecords.Service;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRecords.Controller;

[Route("api/[controller]")]
[ApiController]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordsController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    // GET: api/MedicalRecords
    [HttpGet]
    public async Task<ActionResult<IEnumerable>> GetMedicalRecords()
    {
        var records = await _medicalRecordService.GetAllMedicalRecordsAsync();
        return Ok(records);
    }

    // GET: api/MedicalRecords/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetMedicalRecord(int id)
    {
        var medicalRecord = await _medicalRecordService.GetMedicalRecordByIdAsync(id);

        if (medicalRecord == null)
        {
            return NotFound();
        }

        return Ok(medicalRecord);
    }

    // GET: api/MedicalRecords/person/5
    [HttpGet("person/{personId}")]
    public async Task<ActionResult<IEnumerable>> GetMedicalRecordsByPerson(int personId)
    {
        var records = await _medicalRecordService.GetMedicalRecordsByPersonAsync(personId);
        return Ok(records);
    }

    // GET: api/MedicalRecords/doctor/5
    [HttpGet("doctor/{doctorId}")]
    public async Task<ActionResult<IEnumerable>> GetMedicalRecordsByDoctor(int doctorId)
    {
        var records = await _medicalRecordService.GetMedicalRecordsByDoctorAsync(doctorId);
        return Ok(records);
    }

    // POST: api/MedicalRecords
    [HttpPost]
    public async Task<ActionResult> PostMedicalRecord(MedicalRecord medicalRecord)
    {
        var createdRecord = await _medicalRecordService.CreateMedicalRecordAsync(medicalRecord);
        return CreatedAtAction(nameof(GetMedicalRecord), new { id = createdRecord.Id }, createdRecord);
    }

    // PUT: api/MedicalRecords/5
    [HttpPut("{id}")]
    public async Task<StatusCodeResult> PutMedicalRecord(int id, MedicalRecord medicalRecord)
    {
        var result = await _medicalRecordService.UpdateMedicalRecordAsync(id, medicalRecord);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/MedicalRecords/5
    [HttpDelete("{id}")]
    public async Task<StatusCodeResult> DeleteMedicalRecord(int id)
    {
        var result = await _medicalRecordService.DeleteMedicalRecordAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}