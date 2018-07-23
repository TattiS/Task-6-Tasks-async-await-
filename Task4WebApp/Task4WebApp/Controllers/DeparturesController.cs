
using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
	[Route("api/departures")]
	public class DeparturesController : Controller
	{
		
		private readonly AirportService.Services.DepartureService airport;

		public DeparturesController(AirportService.Services.DepartureService service)
		{
			this.airport = service;

		}
		// GET: api/Departures
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var departures = await this.airport.GetDepartures();
				if (departures == null)
				{
					return NotFound();
				}
				return Ok(departures);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}

		// GET: api/Departures/5 {, Name = "Get"}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var departure = await this.airport.GetDepartureById(id);
				if (departure == null)
				{
					return NotFound();
				}
				return Ok(departure);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// POST: api/Departures
		[HttpPost]
		public async Task <IActionResult> Post([FromBody]DepartureDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreateDeparture(value);

					return Ok(result);
				}
				return BadRequest();
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// PUT: api/Departures/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]DepartureDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdateDeparture(value);

					return Ok(result);
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
				var result = await airport.DeleteDeparture(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
	}
}
