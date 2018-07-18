using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
	[Route("api/Planes")]
	public class PlaneController : Controller
	{

		private readonly AirportService.Services.AsyncPlaneService airport;

		public PlaneController(AirportService.Services.AsyncPlaneService service)
		{
			this.airport = service;

		}
		// GET: api/Plane
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var result = await airport.GetPlanes();
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

		// GET: api/Plane/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var plane = await this.airport.GetPlaneById(id);
				if (plane == null)
				{
					return NotFound();
				}
				return Ok(plane);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// POST: api/Plane
		[HttpPost("departure-id/{id}")]
		public async Task<IActionResult> Post(int departId, [FromBody]PlaneDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreatePlane(departId, value);

					return Ok(result);
				}
				return BadRequest(ModelState);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// PUT: api/Plane/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]PlaneDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdatePlane(value);

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
				var result = await airport.DeletePlane(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
	}
}
