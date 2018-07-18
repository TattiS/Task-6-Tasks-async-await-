using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	interface IAsyncFlightService
    {
		Task<FlightDTO> CreateFlight(FlightDTO flight);
		Task<FlightDTO> GetFlightById(int id);
		Task<List<FlightDTO>> GetFlights();
		Task<List<FlightDTO>> GetFlightsByArrival(DateTime time);
		Task<List<FlightDTO>> GetFlightsByDeparture(DateTime time);
		Task<List<FlightDTO>> GetFlightsByDestination(string destination);
		Task<List<FlightDTO>> GetFlightsByPoint(string departurePoint);
		Task<FlightDTO> UpdateFlight(FlightDTO flight);
		Task<int> DeleteFlight(int id);
		
	}
}
