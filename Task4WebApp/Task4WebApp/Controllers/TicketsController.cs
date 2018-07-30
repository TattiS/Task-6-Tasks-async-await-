using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
    [Route("api/Tickets")]
    public class TicketsController : Controller
    {
		
		private readonly AirportService.Services.TicketService airport;

		public TicketsController(AirportService.Services.TicketService service)
		{
			this.airport = service;

		}

		// GET: api/Tickets
		[HttpGet]
        public async Task<IActionResult> Get()
        {
			try
			{
				var result = await airport.GetTickets();
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

		// GET: api/Tickets/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var ticket = await this.airport.GetTicketById(id);
				if (ticket == null)
				{
					return NotFound();
				}
				return Ok(ticket);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}


		//      // GET: api/Tickets/5
		//      [HttpGet("flight-id/{id}")]
		//      public async Task<IActionResult> Get(int id)
		//      {
		//	try
		//	{
		//		var ticket = await this.airport.GetTicketsByFlightId(id);
		//		if (ticket == null)
		//		{
		//			return NotFound();
		//		}
		//		return Ok(ticket);
		//	}
		//	catch (System.Exception ex)
		//	{

		//		return BadRequest(ex.Message);
		//	}
		//}


		// POST: api/Tickets
		[HttpPost]
        public async Task<IActionResult> Post([FromBody]TicketDTO value)
        {
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreateTicket(value.FlightId, value);

					return Ok(result);
				}
				return BadRequest();
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
        
        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TicketDTO value)
        {
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdateTicket(value);

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
				var result = await airport.DeleteTicket(id);
				return Ok();
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
    }
}
