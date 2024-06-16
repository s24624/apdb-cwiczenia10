using WebApplication1.Models;
using WebApplication1.RequestModels;

namespace WebApplication1.Repositories;

public interface IPrescriptionRepository
{
    
    public  Task<Patient> GetPatientById(int id);
    public Task AddPatient(Patient patient);
    public Task AddPrescription(Prescription prescription);
    public Task<Medicament> GetMedicamentById(int id);
    public Task AddPrescriptionMedicament(PrescriptionMedicament prescriptionMedicament);
    public  Task SaveChanges();
    public  Task<ICollection<Prescription>> GetPatientPrescriptions(int id);
    public Task<ICollection<PrescriptionMedicament>> GetMedicaments(int id);
    public Task<Doctor> GetDoctorById(int id);
    public Task<ICollection<PrescriptionMedicament>> GetPrescriptionMedicaments(int prescriptionId);

}