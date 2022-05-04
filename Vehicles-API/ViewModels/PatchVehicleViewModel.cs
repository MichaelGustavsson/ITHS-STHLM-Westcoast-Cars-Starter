using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles_API.ViewModels
{
  public class PatchVehicleViewModel
  {
    [Required]
    public int Mileage { get; set; }
    [Required]
    public int ModelYear { get; set; }
  }
}