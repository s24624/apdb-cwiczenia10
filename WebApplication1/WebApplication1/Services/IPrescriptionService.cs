using WebApplication1.RequestModels;
using WebApplication1.ResponseModels;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    public Task<int> AddPrescription(WholePrescriptionRequestModel wholePrescriptionRequestModel);
    public Task<PatientWithPrescriptionsResponseModel> GetPatientsWithPrescriptions(int patientId);
}