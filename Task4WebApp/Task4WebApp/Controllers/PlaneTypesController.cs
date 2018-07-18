using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
	[Route("api/PlaneTypes")]
	public class PlaneTypesController : Controller
	{

		private readonly AirportService.Services.AsyncPlaneTypeService airport;

		public PlaneTypesController(AirportService.Services.AsyncPlaneTypeService service)
		{
			this.airport = service;

		}

		// GET: api/PlaneTypes
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var result = await airport.GetPlaneTypes();
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

		// GET: api/PlaneTypes/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var type = await this.airport.GetPlaneTypeById(id);
				if (type == null)
				{
					return NotFound();
				}
				return Ok(type);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// POST: api/PlaneTypes
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]PlaneTypeDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreatePlaneType(value);

					return Ok(result);
				}
				return BadRequest(ModelState);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// PUT: api/PlaneTypes/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]PlaneTypeDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdateType(value);

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
				var result = await airport.DeletePlaneType(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
	}
}
