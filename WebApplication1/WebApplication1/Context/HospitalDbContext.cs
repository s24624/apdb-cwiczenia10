
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() { }
        
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=2019SBD;" +
                                        "Integrated Security=True;TrustServerCertificate=True");
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



        }
    }
