using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
	[Route("api/Pilots")]
	public class PilotsController : Controller
	{

		private readonly AirportService.Services.AsyncPilotService airport;

		public PilotsController(AirportService.Services.AsyncPilotService service)
		{
			this.airport = service;

		}
		// GET: api/Pilots
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var result = await airport.GetPilots();
				if (result == null)
				{
					return NotFound();
				}
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// GET: api/Pilots/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var pilot = await this.airport.GetPilotById(id);
				if (pilot == null)
				{
					return NotFound();
				}
				return Ok(pilot);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// POST: api/Pilots
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]PilotDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreatePilot(value);

					return Ok();
				}
				return BadRequest();
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// PUT: api/Pilots/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]PilotDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdatePilot(value);

					return Ok();
				}
				return BadRequest();
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var result = await airport.DeletePilot(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
	}
}
