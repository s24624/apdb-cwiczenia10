using System.Runtime.InteropServices.JavaScript;
using WebApplication1.Models;

namespace WebApplication1.ResponseModels;

public class AddedPrescriptionResponseModel
{
    public PatientResponseModel Patient { get; set; }
    public ICollection<MedicamentResponseModel> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}