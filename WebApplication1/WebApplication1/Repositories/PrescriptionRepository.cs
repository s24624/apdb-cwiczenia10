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

    public async Task<Medicament> GetMedicamentById(int id)
    {
        return await _context.Medicaments.FirstOrDefaultAsync(e => e.IdMedicament == id);
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
}