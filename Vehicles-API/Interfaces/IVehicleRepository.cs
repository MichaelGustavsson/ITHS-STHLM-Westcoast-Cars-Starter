using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Interfaces
{
  public interface IVehicleRepository
  {
    // Vi har skapat signaturer för våra metoder.
    public Task<List<VehicleViewModel>> ListAllVehiclesAsync();
    public Task<List<VehicleViewModel>> GetVehicleByMakeAsync(string make);
    public Task<VehicleViewModel?> GetVehicleAsync(int id);
    public Task<VehicleViewModel?> GetVehicleAsync(string regNumber);
    public Task AddVehicleAsync(PostVehicleViewModel model);
    public Task DeleteVehicleAsync(int id);
    public Task UpdateVehicleAsync(int id, PostVehicleViewModel model);
    public Task UpdateVehicleAsync(int id, PatchVehicleViewModel model);
    public Task<bool> SaveAllAsync();
  }
}