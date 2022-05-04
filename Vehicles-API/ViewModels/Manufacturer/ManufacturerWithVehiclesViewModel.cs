using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles_API.ViewModels.Manufacturer
{
  public class ManufacturerWithVehiclesViewModel
  {
    public int ManufacturorId { get; set; }
    public string? Name { get; set; }
    public List<VehicleViewModel> Vehicles { get; set; } = new List<VehicleViewModel>();
  }
}