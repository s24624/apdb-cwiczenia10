namespace WebApplication1.ResponseModels;

public class PrescriptionResponseModel
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentResponseModelName> Medicaments { get; set; }
    public int IdDoctor { get; set; }
    public DoctorResponseModel Doctor { get; set; }
    
}