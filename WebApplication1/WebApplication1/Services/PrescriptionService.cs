using System.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.RequestModels;
using WebApplication1.ResponseModels;

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

    public async Task<PatientWithPrescriptionsResponseModel> GetPatientsWithPrescriptions(int patientId)
    {
        var patient = await _prescriptionRepository.GetPatientById(patientId);
        var prescriptions = await _prescriptionRepository.GetPatientPrescriptions(patientId);

        var prescriptionsResponse = new List<PrescriptionResponseModel>();

        foreach (var prescription in prescriptions)
        {
            var medicaments = await _prescriptionRepository.GetPrescriptionMedicaments(prescription.IdPrescription);
            var medicamentsResponse = new List<MedicamentResponseModelName>();

            foreach (var pm in medicaments)
            {
                medicamentsResponse.Add(new MedicamentResponseModelName()
                {
                    IdMedicament = pm.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Details
                });
            }

            var doctor = await _prescriptionRepository.GetDoctorById(prescription.IdDoctor);

            prescriptionsResponse.Add(new PrescriptionResponseModel()
            {
                IdPrescription = prescription.IdPrescription,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                IdDoctor = prescription.IdDoctor,
                Medicaments = medicamentsResponse,
                Doctor = new DoctorResponseModel()
                {
                    IdDoctor = doctor.IdDoctor,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName
                }
            });
        }

        var patientResponse = new PatientWithPrescriptionsResponseModel()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Type = prescriptionsResponse
        };

        return patientResponse;
    }
}
