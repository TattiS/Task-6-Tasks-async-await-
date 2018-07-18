using System.Collections.Generic;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	interface IDepartureService
    {
		void CreateDeparture(DepartureDTO departure);
		DepartureDTO GetDepartureById(int id);
		List<DepartureDTO> GetDepartures();
		void UpdateDeparture(DepartureDTO departure);
		void DeleteDeparture(int id);

	}
}
