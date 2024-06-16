namespace WebApplication1.ResponseModels;

public class PatientWithPrescriptionsResponseModel
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<PrescriptionResponseModel> Type { get; set; }
}