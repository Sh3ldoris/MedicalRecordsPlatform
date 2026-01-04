using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.Model;

public class Doctor
{
    public int Id { get; set; }

    [Required]
    public int PersonId { get; set; }

    [Required]
    [StringLength(50)]
    public string LicenseNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Specialization { get; set; } = string.Empty;

    public DateTime HireDate { get; set; }

    // Navigation properties
    public Person Person { get; set; } = null!;
    public ICollection<MedicalRecord> AssignedMedicalRecords { get; set; } = new List<MedicalRecord>();
}