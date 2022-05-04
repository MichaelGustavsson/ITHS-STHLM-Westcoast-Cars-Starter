using System.ComponentModel.DataAnnotations;

namespace Vehicles_API.ViewModels.Authorization
{
  public class RegisterUserViewModel
  {
    [Required]
    [EmailAddress(ErrorMessage = "Felaktig e-post adress")]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool IsAdmin { get; set; } = false;
  }
}