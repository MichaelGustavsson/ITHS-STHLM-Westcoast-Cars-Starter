using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Repositories
{
  public class ManufacturerRepository : IManufacturerRepository
  {
    private readonly VehicleContext _context;
    private readonly IMapper _mapper;
    public ManufacturerRepository(VehicleContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddManufacturerAsync(PostManufacturerViewModel model)
    {
      var make = _mapper.Map<Manufacturer>(model);
      await _context.Manufacturers.AddAsync(make);
    }

    public async Task DeleteManufacturer(int id)
    {
      var result = await _context.Manufacturers.FindAsync(id);

      if (result is null) throw new Exception($"Kunde inte hitta tillverkare med id {id}");

      _context.Manufacturers.Remove(result);
    }

    public async Task<ManufacturerViewModel> GetManufacturer(int id)
    {
      return _mapper.Map<ManufacturerViewModel>(await _context.Manufacturers.FindAsync(id));
    }

    public async Task<List<ManufacturerViewModel>> ListManufacturerAsync()
    {
      return await _context.Manufacturers.ProjectTo<ManufacturerViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<List<ManufacturerWithVehiclesViewModel>> ListManufacturersVehicles()
    {
      return await _context.Manufacturers.Include(c => c.Vehicles)
      // Steg 1. Projicera tillverkarens data till vy modell
        .Select(m => new ManufacturerWithVehiclesViewModel
        {
          ManufacturorId = m.Id,
          Name = m.Name,
          Vehicles = m.Vehicles
          // Steg 2. Projicera andra änden av vår join(vehicle data) till en VehicleViewModel
            .Select(v => new VehicleViewModel
            {
              VehicleId = v.Id,
              RegNo = v.RegNo,
              VehicleName = string.Concat(v.Manufacturer.Name, " ", v.Model),
              ModelYear = v.ModelYear,
              Mileage = v.Mileage
            }).ToList()
        })
        .ToListAsync();
    }

    public async Task<ManufacturerWithVehiclesViewModel?> ListManufacturersVehicles(int id)
    {
      return await _context.Manufacturers.Where(c => c.Id == id).Include(c => c.Vehicles)
      // Steg 1. Projicera tillverkarens data till vy modell
        .Select(m => new ManufacturerWithVehiclesViewModel
        {
          ManufacturorId = m.Id,
          Name = m.Name,
          Vehicles = m.Vehicles
          // Steg 2. Projicera andra änden av vår join(vehicle data) till en VehicleViewModel
            .Select(v => new VehicleViewModel
            {
              VehicleId = v.Id,
              RegNo = v.RegNo,
              VehicleName = string.Concat(v.Manufacturer.Name!, " ", v.Model!),
              ModelYear = v.ModelYear,
              Mileage = v.Mileage
            }).ToList()
        })
        .SingleOrDefaultAsync();
    }
    public async Task UpdateManufacturer(int id, PutManufacturerViewModel model)
    {
      var make = await _context.Manufacturers.FindAsync(id);

      if (make is null) throw new Exception($"Kunde inte hitta någon tillverkare med namnet {model.Name} i vårt system");

      make.Name = model.Name;

      _context.Manufacturers.Update(make);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

  }
}