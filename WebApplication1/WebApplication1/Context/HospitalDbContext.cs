
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() { }
        
        public virtual DbSet<Doctor> Doctors { get; set; }
        
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
        }
    }
