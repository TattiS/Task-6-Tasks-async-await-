using DALProject;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
    [Route("api/flights")]
    public class FlightController : Controller
    {
		//private readonly AirportService.AirportService airport;
		//private readonly AirportService.BLLService airport;
		//public FlightController(AirportService.BLLService service)
		//{
		//	this.airport = service;

		//}
		private readonly AirportService.Services.FlightService airport;

		public FlightController(AirportService.Services.FlightService service)
		{
			this.airport = service;

		}

		// GET: api/flights
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				var result = airport.GetFlights();
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
        public IActionResult Get(int id)
        {
			try
			{
				var flight = this.airport.GetFlightById(id);
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
        public IActionResult Post([FromBody]FlightDTO value)
        {
			try
			{
				if (ModelState.IsValid)
				{
					airport.CreateFlight(value);
				
					return Ok();
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
        public IActionResult Put(int id, [FromBody]FlightDTO value)
        {
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					airport.UpdateFlight(value);

					return Ok();
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
        public IActionResult Delete(int id)
        {
			try
			{
				airport.DeleteFlight(id);
				return Ok();
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}

		}
	}
}
