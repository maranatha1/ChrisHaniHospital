using System.Reflection.Emit;
using ChrisHaniHospital.Areas.Identity.Data;
using ChrisHaniHospital.Controllers;
using ChrisHaniHospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChrisHaniHospital.Data;

public class ChrisHaniContext : IdentityDbContext<ChrisHaniUser>
{
    public ChrisHaniContext(DbContextOptions<ChrisHaniContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<ChrisHaniHospital.Models.Country> Country { get; set; } = default!;

    public DbSet<ChrisHaniHospital.Models.Province> Province { get; set; } = default!;

    public DbSet<ChrisHaniHospital.Models.City> City { get; set; } = default!;

    public DbSet<ChrisHaniHospital.Models.Surbub> Surbub { get; set; } = default!;
    public DbSet<Patient> Patients { get; set; } = default!;
    public DbSet<Surgeon> Surgeons { get; set; } = default!;
    public DbSet<Nurse> Nurses { get; set; } = default!;
    public DbSet<Anaesthesiologist> Anaesthesiologists { get; set; } = default!;
    public DbSet<Pharmacist> Pharmacists { get; set; } = default!;

    public DbSet<ChrisHaniHospital.Models.Medication> Medication { get; set; } = default!;
    public DbSet<ChrisHaniHospital.Models.PrescriptionMedication> PrescriptionMedications { get; set; } =
        default!;

    public DbSet<ChrisHaniHospital.Models.Conditions> Conditions { get; set; } = default!;

    public DbSet<ChrisHaniHospital.Models.PatientVitals> PatientVitals { get; set; } = default!;

    public DbSet<ChrisHaniHospital.Models.Allergies> Allergies { get; set; } = default!;

    public DbSet<ChrisHaniHospital.Models.AdmittedPatient> AdmittedPatients { get; set; } =
        default!;

    public DbSet<ChrisHaniHospital.Models.Ward> Ward { get; set; } = default!;
    public DbSet<ChrisHaniHospital.Models.PatientAllergies> PatientAllergies { get; set; } =
        default!;
    public DbSet<ChrisHaniHospital.Models.PatientConditions> PatientConditions { get; set; } =
        default!;
    public DbSet<ChrisHaniHospital.Models.PatientMedication> PatientMedications { get; set; } =
        default!;

    public DbSet<ChrisHaniHospital.Models.Precription> Precription { get; set; } = default!;
    public DbSet<ChrisHaniHospital.Models.AdministeredMedication> administeredMedications { get; set; } =
        default!;
}
