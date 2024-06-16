namespace WebApplication1.RequestModels;

public class WholePrescriptionRequestModel
{
    public PatientRequestModel PatientRequestModel { get; set; }
    public ICollection<MedicamentRequestModel> Medicaments { get; set; }
    public int IdDoctr { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}