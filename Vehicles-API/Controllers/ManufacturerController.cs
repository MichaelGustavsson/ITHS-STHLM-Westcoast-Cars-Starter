using Microsoft.AspNetCore.Mvc;
using Vehicles_API.Interfaces;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Controllers
{
  [ApiController]
  [Route("api/v1/manufacturers")]
  public class ManufacturerController : ControllerBase
  {
    // Deklarera en referens variabel för att hålla en konkret instans av en klass
    // som implementer interface IManufacturerRepository
    private readonly IManufacturerRepository _repo;
    // Skapa en konstruktor med dependency injicering av en implementering av interface IManufacturerRepository
    public ManufacturerController(IManufacturerRepository repo)
    {
      _repo = repo;
    }

    // Skapa en endpoint som lyssnar efter url: api/v1/manufacturers/list
    [HttpGet("list")]
    public async Task<ActionResult> ListAllManufacturers()
    {
      // Anropa metoden ListManufacturerAsync i vårt repository.
      var list = await _repo.ListManufacturerAsync();
      return Ok(list);
    }

    // Skapa en endpoint som lyssnar efter url: api/v1/manufacturers/id
    [HttpGet("{id}")]
    public async Task<ActionResult> GetManufacturerById(int id)
    {
      // Anropa metoden GetManufacturer i vårt repository
      return Ok(await _repo.GetManufacturer(id));
    }

    // Skapa en endpoint som lyssnar efter url: api/v1/manufacturers/vehicles
    // För att hämta alla tillverkare och alla bilar för respektive tillverkare...
    [HttpGet("vehicles")]
    public async Task<ActionResult> ListManufacturersAndVehicles()
    {
      // Anropa metoden ListManufacturersVehicles i vårt repository
      return Ok(await _repo.ListManufacturersVehicles());
    }

    // Skapa en endpoint som lyssnar efter url: api/v1/manufacturers/id/vehicles
    // För att hämta en utvald tillverkare och alla dess bilar
    [HttpGet("{id}/vehicles")]
    public async Task<ActionResult> ListManufacturerVehicles(int id)
    {
      // Anropa metoden ListManufacturersVehicles i vårt repository
      var result = await _repo.ListManufacturersVehicles(id);

      // Kontrollera om vi har hittat någon tillverkare
      if (result is null)
        // Annars returnera ett 404 NotFound felmeddelande
        return NotFound($"Vi kunde inte hitta någon tillverkare med id {id}");

      // Annars returnera tillverkaren med dess kopplade bilar.
      return Ok(result);
    }

    // Skapa en endpoint som lyssnar på POST anrop för att lägga till en ny tillverkare.
    // Metoden tar som argument en instans av Vy modellen PostManufacturerViewModel
    [HttpPost()]
    public async Task<ActionResult> AddManufacturer(PostManufacturerViewModel model)
    {
      try
      {
        // Anropa metoden AddManufacturerAsync i vårt repository och lägg till bilen/fordonet i EntityFrameworkCore's ChangeTracking.
        await _repo.AddManufacturerAsync(model);

        // Anropa metoden SaveAllAsync i vårt repository för att spara ner ändringar i databasen.
        if (await _repo.SaveAllAsync())
        {
          // Returnera statuskoden 201(Created)
          return StatusCode(201);
        }

        // Om det inte gick att spara returnera ett server fel(500).
        return StatusCode(500, "Det gick fel när vi skulle spara tillverkaren");
      }
      catch (Exception ex)
      {
        // Om vi hamnar här så betyder det att ett fel har inträffat
        // som inte är hanterat nedåt i anropskedjan(stacken).
        // Därför returnerar vi ett server fel(500 Internal Server Error).
        return StatusCode(500, ex.Message);
      }
    }

    // Skapa en endpoint som lyssnar på PUT anrop url: api/v1/manufacturers/id
    // för att kunna uppdatera en befintlig tillverkare.
    // Skicka med datat som skall uppdateras i metodens anrop i vårt fall en instans av
    // PutManufacturerViewModel klassen.
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateManufacturer(int id, PutManufacturerViewModel model)
    {
      // Kapsla hela anropet i ett try...catch block
      // Anledningen är att metoden i vårt repository kastar ett exception om vi inte kan hitta bilen
      // som skall uppdateras...
      try
      {
        await _repo.UpdateManufacturer(id, model);

        // Glöm inte att anropa SaveAllAsync för att spara ner ändringarna till databasen.
        if (await _repo.SaveAllAsync())
        {
          // Om allt gick bra returnera status kod 204(NoContent), vi har inget att rapportera
          return NoContent();
        }

        // Annars returnera status kod 500 (Internal Server Error)
        return StatusCode(500, $"Något gick fel och det gick inte att uppdatera tillverkare {model.Name}");
      }
      catch (Exception ex)
      {
        // Om vi hamnar här så har ett exception kastats ifrån metoden i vårt repository
        // och vi returnerar ett Internal Server Error meddelande.
        return StatusCode(500, ex.Message);
      }
    }

    // Skapa en endpoint som lyssnar på DELETE anrop url: api/v1/manufacturers/id
    // för att kunna ta bort en tillverkare ur systemet och databasen.
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteManufacturer(int id)
    {
      // Kapsla hela anropet i ett try...catch block
      // Anledningen är att metoden i vårt repository kastar ett exception om vi inte kan hitta tillverkaren
      // som skall tas bort...
      try
      {
        await _repo.DeleteManufacturer(id);

        // Glöm inte att anropa SaveAllAsync för att spara ner ändringarna till databasen.
        if (await _repo.SaveAllAsync())
        {
          // Om allt gick bra returnera status kod 204(NoContent), vi har inget att rapportera
          return NoContent();
        }

        // Annars returnera status kod 500 (Internal Server Error)
        return StatusCode(500, $"Det gick inte att ta bort tillverkare med id {id}");
      }
      catch (Exception ex)
      {
        // Om vi hamnar här så har ett exception kastats ifrån metoden i vårt repository
        // och vi returnerar ett Internal Server Error meddelande.
        return StatusCode(500, ex.Message);
      }
    }
  }
}