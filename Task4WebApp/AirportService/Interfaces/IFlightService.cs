using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface IFlightService
    {
		void CreateFlight(FlightDTO flight);
		FlightDTO GetFlightById(int id);
		List<FlightDTO> GetFlights();
		List<FlightDTO> GetFlightsByArrival(DateTime time);
		List<FlightDTO> GetFlightsByDeparture(DateTime time);
		List<FlightDTO> GetFlightsByDestination(string destination);
		List<FlightDTO> GetFlightsByPoint(string departurePoint);
		void UpdateFlight(FlightDTO flight);
		void DeleteFlight(int id);
		
	}
}
