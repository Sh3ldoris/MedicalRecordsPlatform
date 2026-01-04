using Microsoft.EntityFrameworkCore;
using MedicalRecords.Model;

namespace MedicalRecords.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Person configuration
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
                
            // One Person to One Doctor relationship
            entity.HasOne(p => p.Doctor)
                .WithOne(d => d.Person)
                .HasForeignKey("Person", "PersonId")
                .OnDelete(DeleteBehavior.Cascade);

            // One Person to Many MedicalRecords relationship
            entity.HasMany(p => p.MedicalRecords)
                .WithOne(mr => mr.Person)
                .HasForeignKey(mr => mr.PersonId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Doctor configuration
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.LicenseNumber).IsUnique();

            // One Doctor to Many MedicalRecords relationship
            entity.HasMany(d => d.AssignedMedicalRecords)
                .WithOne(mr => mr.Doctor)
                .HasForeignKey(mr => mr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // MedicalRecord configuration
        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}