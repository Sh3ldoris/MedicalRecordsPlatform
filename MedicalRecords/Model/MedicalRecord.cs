using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.Model;

public class MedicalRecord
{
    public int Id { get; set; }

    [Required]
    public int PersonId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateTime RecordDate { get; set; }

    [Required]
    [StringLength(200)]
    public string Diagnosis { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Treatment { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    // Navigation properties
    public Person Person { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
}