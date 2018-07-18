using System;
using System.Collections.Generic;
using DTOLibrary.DTOs;

namespace AirportService
{
	public interface IBLLService
	{
		void CreateCrew(int departId, CrewDTO value);
		void CreateDeparture(DepartureDTO departure);
		void CreateFlight(FlightDTO flight);
		void CreatePilot(PilotDTO pilot);
		void CreatePlane(int departId, PlaneDTO value);
		void CreatePlaneType(PlaneTypeDTO planeType);
		void CreateStewardess(StewardessDTO stewardess);
		void CreateTicket(int flightId, TicketDTO value);
		void DeleteCrew(int id);
		void DeleteDeparture(int id);
		void DeleteFlight(int id);
		void DeletePilot(int id);
		void DeletePlane(int id);
		void DeletePlaneType(int id);
		void DeleteStewardess(int id);
		void DeleteTicket(int id);
		CrewDTO GetCrewById(int id);
		List<CrewDTO> GetCrews();
		List<CrewDTO> GetCrewsBy(Predicate<CrewDTO> predicate);
		DepartureDTO GetDepartureById(int id);
		List<DepartureDTO> GetDepartures();
		FlightDTO GetFlightById(int id);
		List<FlightDTO> GetFlights();
		List<FlightDTO> GetFlightsByArrival(DateTime time);
		List<FlightDTO> GetFlightsByDeparture(DateTime time);
		List<FlightDTO> GetFlightsByDestination(string destination);
		List<FlightDTO> GetFlightsByPoint(string departurePoint);
		PilotDTO GetPilotById(int id);
		List<PilotDTO> GetPilots();
		PlaneDTO GetPlaneById(int id);
		List<PlaneDTO> GetPlanes();
		PlaneTypeDTO GetPlaneTypeById(int id);
		List<PlaneTypeDTO> GetPlaneTypes();
		StewardessDTO GetStewardessById(int id);
		List<StewardessDTO> GetStewardesses();
		List<TicketDTO> GetTickets();
		List<TicketDTO> GetTicketsByFlightId(int flightId);
		void UpdateCrew(CrewDTO value);
		void UpdateDeparture(DepartureDTO departure);
		void UpdateFlight(FlightDTO flight);
		void UpdatePilot(PilotDTO pilot);
		void UpdatePlane(PlaneDTO value);
		void UpdateStewardess(StewardessDTO stewardess);
		void UpdateTicket(TicketDTO value);
		void UpdateType(PlaneTypeDTO planeType);
	}
}