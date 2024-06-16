using System.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.RequestModels;

namespace WebApplication1.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<int> AddPrescription(WholePrescriptionRequestModel wholePrescriptionRequestModel)
    {
        var patient = await _prescriptionRepository.GetPatientById(wholePrescriptionRequestModel.PatientRequestModel.IdPatient);
        if (patient == null)
        {
            patient = new Patient()
            {
                FirstName = wholePrescriptionRequestModel.PatientRequestModel.FirstName,
                LastName = wholePrescriptionRequestModel.PatientRequestModel.LastName,
                BirthDate = wholePrescriptionRequestModel.PatientRequestModel.BirthDate,
                Prescriptions = new List<Prescription>()
            };

            await _prescriptionRepository.AddPatient(patient);
        }

        foreach (var med in wholePrescriptionRequestModel.Medicaments)
        {
            var medicament = await _prescriptionRepository.GetMedicamentById(med.IdMedicament);
            if (medicament == null)
            {
                throw new DataException("Medicament does not exist");
            }
        }

        var prescription = new Prescription()
        {
            Date = wholePrescriptionRequestModel.Date,
            DueDate = wholePrescriptionRequestModel.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = wholePrescriptionRequestModel.IdDoctr,
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        await _prescriptionRepository.AddPrescription(prescription);

        var numberOfMedicaments = wholePrescriptionRequestModel.Medicaments.Count();
        if (numberOfMedicaments > 10)
        {
            throw new DataException("You cannot add more than 10 medicaments to a prescription");
        }

        foreach (var med in wholePrescriptionRequestModel.Medicaments)
        {
            await _prescriptionRepository.AddPrescriptionMedicament(new PrescriptionMedicament()
            {
                IdMedicament = med.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = med.Dose,
                Details = med.Description
            });
        }

        await _prescriptionRepository.SaveChanges();

        return prescription.IdPrescription;
    }
}
