namespace MedicalRecordsUI.Models;

public class MedicalRecord
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int DoctorId { get; set; }
    public DateTime RecordDate { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string? Treatment { get; set; }
    public string? Notes { get; set; }
    public Person? Person { get; set; }
    public Doctor? Doctor { get; set; }
}
