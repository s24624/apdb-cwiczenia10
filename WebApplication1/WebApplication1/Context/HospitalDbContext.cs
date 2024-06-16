
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() { }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }
        
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source=localhost, 1433; User=SA; Password=yourStrong(!)Password; Initial Catalog=apdb6; Integrated Security=False;Connect Timeout=30;Encrypt=False");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);

                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();

            });
            modelBuilder.Entity<Patient>(opt =>
            {
                opt.HasKey(e => e.IdPatient);
                opt.Property(e => e.FirstName ).HasMaxLength(100).IsRequired();
                opt.Property(e => e.LastName ).HasMaxLength(100).IsRequired();
                opt.Property(e => e.BirthDate ).IsRequired(); 
            });
            modelBuilder.Entity<Medicament>(opt =>
            {
                opt.HasKey(e => e.IdMedicament);
                opt.Property(e=>e.Name).HasMaxLength(100).IsRequired();
                opt.Property(e=>e.Description).HasMaxLength(100).IsRequired();
                opt.Property(e=>e.Type).HasMaxLength(100).IsRequired();
                
            });
            modelBuilder.Entity<Prescription>(opt =>
            {
                opt.HasKey(e => e.IdPrescription);
               
                opt.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(
                e=>e.IdDoctor);
                opt.HasOne(e=>e.Patient).
                    WithMany(e=>e.Prescriptions).HasForeignKey(e=>e.IdPatient);
            });
            modelBuilder.Entity<PrescriptionMedicament>(opt =>
            {
                opt.HasKey(e=> new
                {
                    e.IdPrescription,
                    e.IdMedicament
                });
                opt.Property(e => e.Dose);
                opt.Property(e => e.Details).IsRequired().HasMaxLength(100);
                opt.HasOne(e=>e.Medicament).
                    WithMany(e=>e.PrescriptionMedicaments).HasForeignKey(
                        e=>e.IdMedicament);
                opt.HasOne(e=>e.Prescription).
                    WithMany(e=>e.PrescriptionMedicaments).HasForeignKey(
                        e=>e.IdPrescription);
            });



        }
    }
