using System.ComponentModel.DataAnnotations;

namespace Vehicles_API.ViewModels
{
  public class PostVehicleViewModel
  {
    [Required(ErrorMessage = "Registreringsnummer är obligatoriskt")]
    public string? RegNo { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int ModelYear { get; set; }
    public int Mileage { get; set; }
  }
}