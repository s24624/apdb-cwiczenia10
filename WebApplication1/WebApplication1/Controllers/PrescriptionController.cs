using Microsoft.AspNetCore.Mvc;
using WebApplication1.RequestModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] WholePrescriptionRequestModel wholePrescriptionRequestModel)
    {
        try
        {
            var prescriptionId = await _prescriptionService.AddPrescription(wholePrescriptionRequestModel);
            return Ok(new { Message = "Prescription successfully added", PrescriptionId = prescriptionId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}