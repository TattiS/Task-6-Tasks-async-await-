using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
	[Route("api/flights")]
	public class FlightController : Controller
	{

		private readonly AirportService.Services.FlightService airport;

		public FlightController(AirportService.Services.FlightService service)
		{
			this.airport = service;

		}

		// GET: api/flights
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var result = await airport.GetFlights();
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

		// GET: api/Flight/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var flight = await this.airport.GetFlightById(id);
				if (flight == null)
				{
					return NotFound();
				}
				return Ok(flight);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}

		// GET: api/Flight/delay/
		[HttpGet("delay/")]
		public async Task<IActionResult> GetWithDelay()
		{
			try
			{
				var flight = await this.airport.RunAsync();
				if (flight == null)
				{
					return NotFound();
				}
				return Ok(flight);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}

		// POST: api/Flight
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]FlightDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreateFlight(value);

					return Ok(result);
				}
				return BadRequest(ModelState);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}


		}

		// PUT: api/Flight/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]FlightDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdateFlight(value);

					return Ok(result);
				}
				return BadRequest(ModelState);
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
				var result = await airport.DeleteFlight(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}
	}
}
