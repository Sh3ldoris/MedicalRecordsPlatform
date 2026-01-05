namespace MedicalRecordsUI.Models;

public class Doctor
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public Person? Person { get; set; }
}
