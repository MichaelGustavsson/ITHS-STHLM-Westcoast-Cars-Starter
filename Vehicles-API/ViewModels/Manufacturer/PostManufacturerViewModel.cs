using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles_API.ViewModels.Manufacturer
{
  public class PostManufacturerViewModel
  {
    [Required]
    public string? Name { get; set; }
  }
}