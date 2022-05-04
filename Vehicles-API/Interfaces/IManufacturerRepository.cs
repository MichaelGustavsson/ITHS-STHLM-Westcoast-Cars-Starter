using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Interfaces
{
  public interface IManufacturerRepository
  {
    public Task AddManufacturerAsync(PostManufacturerViewModel model);
    public Task<List<ManufacturerViewModel>> ListManufacturerAsync();
    public Task<ManufacturerViewModel> GetManufacturer(int id);
    public Task<List<ManufacturerWithVehiclesViewModel>> ListManufacturersVehicles();
    public Task<ManufacturerWithVehiclesViewModel?> ListManufacturersVehicles(int id);
    public Task UpdateManufacturer(int id, PutManufacturerViewModel model);
    public Task DeleteManufacturer(int id);
    public Task<bool> SaveAllAsync();
  }
}