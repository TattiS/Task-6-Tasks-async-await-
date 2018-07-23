using System.Collections.Generic;
using System.Threading.Tasks;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	interface IDepartureService
    {
		Task<DepartureDTO> CreateDeparture(DepartureDTO departure);
		Task<DepartureDTO> GetDepartureById(int id);
		Task<List<DepartureDTO>> GetDepartures();
		Task<DepartureDTO> UpdateDeparture(DepartureDTO departure);
		Task<int> DeleteDeparture(int id);
	}
}
