using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
    [Route("api/Stewardesses")]
    public class StewardessesController : Controller
    {
		private readonly AirportService.Services.StewardessService airport;

		public StewardessesController(AirportService.Services.StewardessService service)
		{
			this.airport = service;

		}
		// GET: api/Stewardesses
		[HttpGet]
        public async Task<IActionResult> Get()
        {
			try
			{
				var result = await airport.GetStewardesses();
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

        // GET: api/Stewardesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
			try
			{
				var stewardess = await this.airport.GetStewardessById(id);
				if (stewardess == null)
				{
					return NotFound();
				}
				return Ok(stewardess);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
        
        // POST: api/Stewardesses
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StewardessDTO value)
        {
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreateStewardess(value);

					return Ok(result);
				}
				return BadRequest();
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
        
        // PUT: api/Stewardesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]StewardessDTO value)
        {
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdateStewardess(value);

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
				var result = await airport.DeleteStewardess(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
    }
}
