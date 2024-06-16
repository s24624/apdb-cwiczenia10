using WebApplication1.RequestModels;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    public Task<int> AddPrescription(WholePrescriptionRequestModel wholePrescriptionRequestModel);
}