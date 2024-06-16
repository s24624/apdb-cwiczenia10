using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly HospitalDbContext _context;

    public PrescriptionRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<Patient> GetPatientById(int id)
    {
        return await _context.Patients.FirstOrDefaultAsync(e => e.IdPatient == id);
    }

    public async Task AddPatient(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await SaveChanges();
    }

    public async Task AddPrescription(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await SaveChanges();
    }

    public async Task<ICollection<PrescriptionMedicament>> GetMedicaments(int id)
    {
        return await _context.PrescriptionMedicaments
            .Where(e => e.Prescription.IdPatient == id)
            .ToListAsync();
    }

    public async Task<ICollection<PrescriptionMedicament>> GetPrescriptionMedicaments(int prescriptionId)
    {
        return await _context.PrescriptionMedicaments
            .Include(pm => pm.Medicament)
            .Where(pm => pm.IdPrescription == prescriptionId)
            .ToListAsync();
    }

    public async Task<Medicament> GetMedicamentById(int id)
    {
        return await _context.Medicaments.FirstOrDefaultAsync(e => e.IdMedicament == id);
    }

    public async Task<Doctor> GetDoctorById(int id)
    {
        return await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == id);
    }

    public async Task AddPrescriptionMedicament(PrescriptionMedicament prescriptionMedicament)
    {
        await _context.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
        await SaveChanges();
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Prescription>> GetPatientPrescriptions(int id)
    {
        return await _context.Prescriptions
            .Where(e => e.IdPatient == id)
            .ToListAsync();
    }
}
