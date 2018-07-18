using System;
using System.Collections.Generic;
using AutoMapper;

using DALProject;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService
{
	public class BLLService : IBLLService
	{
		private static UnitOfWork unit;
		private static IMapper mapper;

		public BLLService(MainDBContext dBContext)
		{
			unit = new UnitOfWork(dBContext);

			var mapConfig = new MapperConfiguration(c =>
			{
				c.CreateMap<Flight, FlightDTO>().ReverseMap();
				c.CreateMap<Departure, DepartureDTO>().ReverseMap();
				c.CreateMap<Ticket, TicketDTO>().ReverseMap();
				c.CreateMap<Crew, CrewDTO>().ReverseMap();
				c.CreateMap<Pilot, PilotDTO>().ForMember(e => e.StartedIn, opt => opt.Ignore());
				c.CreateMap<PilotDTO, Pilot>().ForMember(e => e.Experience, opt => opt.MapFrom(src => (DateTime.Today.Subtract(src.StartedIn))))
											  .ForMember(e=> e.TimeTicks, opt=>opt.Ignore());
				c.CreateMap<Plane, PlaneDTO>().ForMember(e => e.ExpiryDate, opt => opt.Ignore());
				c.CreateMap<PlaneDTO, Plane>().ForMember(e => e.OperationLife, opt => opt.MapFrom(src => (src.ExpiryDate.Subtract(src.ReleaseDate))))
											  .ForMember(e=> e.TimeTicks, opt=> opt.Ignore());
				c.CreateMap<Stewardess, StewardessDTO>().ReverseMap();
				c.CreateMap<PlaneType, PlaneTypeDTO>().ReverseMap();
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

			
		}

		public void CreateCrew(int departId, CrewDTO value)
		{
			var departure = unit.DeparturesRepo.GetEntityById(departId);
			if (departure != null)
			{
				var crew = mapper.Map<CrewDTO, Crew>(value);
				if (crew == null)
				{
					throw new Exception("Error: Can't add this crew to the the departure!");
				}
				departure.CrewItem = crew;
				unit.DeparturesRepo.Update(departure);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Can't find such departure!");
			}
		}

		public void CreateDeparture(DepartureDTO departure)
		{
			if (departure != null)
			{
				Departure newDepart = mapper.Map<DepartureDTO, Departure>(departure);
				unit.DeparturesRepo.Insert(newDepart);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void CreateFlight(FlightDTO flight)
		{
			if (flight == null)
			{
				Flight newFlight = mapper.Map<FlightDTO, Flight>(flight);
				unit.FlightsRepo.Insert(newFlight);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void CreatePilot(PilotDTO pilot)
		{
			if (pilot != null)
			{
				Pilot newPilot = mapper.Map<PilotDTO, Pilot>(pilot);
				unit.PilotsRepo.Insert(newPilot);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void CreatePlane(int departId, PlaneDTO value)
		{
			var departure = unit.DeparturesRepo.GetEntityById(departId);
			if (departure != null)
			{
				var plane = mapper.Map<PlaneDTO, Plane>(value);
				if (plane == null)
				{
					throw new Exception("Error: Can't add this crew to the the departure!");
				}
				departure.PlaneItem = plane;
				unit.DeparturesRepo.Update(departure);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Can't find such departure!");
			}
		}

		public void CreatePlaneType(PlaneTypeDTO planeType)
		{
			if (planeType != null)
			{
				PlaneType newPlaneType = mapper.Map<PlaneTypeDTO, PlaneType>(planeType);
				unit.PlaneTypesRepo.Insert(newPlaneType);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void CreateStewardess(StewardessDTO stewardess)
		{
			if (stewardess != null)
			{
				Stewardess newStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess);
				unit.StewardessesRepo.Insert(newStewardess);
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void CreateTicket(int flightId, TicketDTO value)
		{
			var flight = unit.FlightsRepo.GetEntityById(flightId);
			if (flight != null)
			{
				var ticket = mapper.Map<TicketDTO, Ticket>(value);
				if (ticket == null)
				{
					throw new Exception("Error: Can't add this ticket to the the flight!");
				}
				flight.Tickets.Add(ticket);
				unit.FlightsRepo.Update(flight);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Can't find such flight!");
			}
		}

		public void DeleteCrew(int id)
		{
			var itemToDelete = unit.DeparturesRepo.GetEntities().Find(p => p.CrewItem.Id == id);
			if (itemToDelete != null)
			{
				itemToDelete.CrewItem = null;
				unit.DeparturesRepo.Update(itemToDelete);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Can't find such crew!");
			}
		}

		public void DeleteDeparture(int id)
		{

			var departureToDelete = unit.DeparturesRepo.GetEntityById(id);
			if (departureToDelete != null)
			{
				unit.DeparturesRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such departure to delete.");
			}
		}

		public void DeleteFlight(int id)
		{
			var flightToDelete = unit.FlightsRepo.GetEntityById(id);
			if (flightToDelete != null)
			{
				unit.FlightsRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such flight to delete.");
			}
		}

		public void DeletePilot(int id)
		{
			var pilotToDelete = unit.PilotsRepo.GetEntityById(id);
			if (pilotToDelete != null)
			{
				unit.PilotsRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such pilot to delete.");
			}
		}

		public void DeletePlane(int id)
		{
			var itemToDelete = unit.DeparturesRepo.GetEntities().Find(p => p.PlaneItem.Id == id);
			if (itemToDelete != null)
			{
				itemToDelete.PlaneItem = null;
				unit.DeparturesRepo.Update(itemToDelete);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Can't find such plane to delete!");
			}
		}

		public void DeletePlaneType(int id)
		{
			var typeToDelete = unit.PlaneTypesRepo.GetEntityById(id);
			if (typeToDelete != null)
			{
				unit.PlaneTypesRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such type of plane to delete.");
			}
		}

		public void DeleteStewardess(int id)
		{
			var stewardessToDelete = unit.StewardessesRepo.GetEntityById(id);
			if (stewardessToDelete != null)
			{
				unit.StewardessesRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such stewardess to delete.");
			}
		}

		public void DeleteTicket(int id)
		{

			var itemToDelete = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").Find(p => p.Tickets.Find(i => i.Id == id) != null);
			if (itemToDelete != null)
			{
				itemToDelete.Tickets.RemoveAll(p => p.Id == id);
				unit.FlightsRepo.Update(itemToDelete);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Can't find such ticket!");
			}
		}

		public CrewDTO GetCrewById(int id)
		{
			return GetCrews()?.Find(p => p.Id == id);
		}

		public List<CrewDTO> GetCrews()
		{
			List<Crew> crews = new List<Crew>();
			foreach (var i in unit.DeparturesRepo.GetEntities(includeProperties: "CrewItem,PlaneItem"))
			{
				if (i.CrewItem != null)
				{ crews.Add(i.CrewItem); }
			}
			return mapper.Map<List<Crew>, List<CrewDTO>>(crews);
		}

		public List<CrewDTO> GetCrewsBy(Predicate<CrewDTO> predicate)
		{
			return GetCrews()?.FindAll(predicate);
		}

		public DepartureDTO GetDepartureById(int id)
		{
			Departure departure = unit.DeparturesRepo.GetEntityById(id);
			if (departure == null)
			{
				return null;
			}
			return mapper.Map<Departure, DepartureDTO>(departure);
		}

		public List<DepartureDTO> GetDepartures()
		{
			List<Departure> result = unit.DeparturesRepo.GetEntities(includeProperties: "CrewItem,PlaneItem");
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Departure>, List<DepartureDTO>>(result);
		}

		public FlightDTO GetFlightById(int id)
		{
			Flight subresult = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").Find(p => p.Id == id);
			if (subresult == null)
			{
				return null;
			}
				
			FlightDTO result = mapper.Map<Flight, FlightDTO>(subresult);
			return result;
		}

		public List<FlightDTO> GetFlights()
		{

			List<Flight> subresult = unit.FlightsRepo.GetEntities(includeProperties: "Tickets");
			List<FlightDTO> result = null;
			if (subresult != null)
			{
				result = mapper.Map<List<Flight>, List<FlightDTO>>(subresult);
			}
			return result;
		}

		public List<FlightDTO> GetFlightsByArrival(DateTime time)
		{
			List<Flight> subresult = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").FindAll(f => f.ArrivalTime == time);
			if (subresult == null)
			{
				return null;
			}
			return mapper.Map<List<Flight>, List<FlightDTO>>(subresult);
		}

		public List<FlightDTO> GetFlightsByDeparture(DateTime time)
		{
			List<Flight> result = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").FindAll(f => f.DepartureTime == time);
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Flight>, List<FlightDTO>>(result);
		}

		public List<FlightDTO> GetFlightsByDestination(string destination)
		{
			
			List<Flight> result = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").FindAll(f => f.Destination == destination);
			if (result == null)
			{
				return null;
			}

			return mapper.Map<List<Flight>, List<FlightDTO>>(result);
		}

		public List<FlightDTO> GetFlightsByPoint(string departurePoint)
		{
			List<Flight> result = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").FindAll(f => f.DeparturePoint == departurePoint) ;
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Flight>, List<FlightDTO>>(result);
		}

		public PilotDTO GetPilotById(int id)
		{
			Pilot pilot = unit.PilotsRepo.GetEntityById(id);
			if (pilot == null)
			{
				return null;
			}
			return mapper.Map<Pilot, PilotDTO>(pilot);
		}

		public List<PilotDTO> GetPilots()
		{
			List<Pilot> pilots = unit.PilotsRepo.GetEntities();
			if (pilots == null)
			{
				return null;
			}
			return mapper.Map<List<Pilot>, List<PilotDTO>>(pilots);
		}

		public PlaneDTO GetPlaneById(int id)
		{
			return GetPlanes().Find(p => p.Id == id);
		}

		public List<PlaneDTO> GetPlanes()
		{
			var departures = unit.DeparturesRepo.GetEntities(includeProperties: "CrewItem,PlaneItem").FindAll(p => p.PlaneItem != null);
			List<Plane> planes = new List<Plane>();
			if (departures != null && departures.Count > 0)
			{
				foreach (var item in departures)
				{
					planes.Add(item.PlaneItem);
				}
				return mapper.Map<List<Plane>, List<PlaneDTO>>(planes);
			}
			else
			{
				throw new Exception("Error: There isn't any plane.");
			}
		}

		public PlaneTypeDTO GetPlaneTypeById(int id)
		{
			PlaneType type = unit.PlaneTypesRepo.GetEntityById(id);
			if (type == null)
			{
				return null;
			}
			return mapper.Map<PlaneType, PlaneTypeDTO>(type);
		}

		public List<PlaneTypeDTO> GetPlaneTypes()
		{
			List<PlaneType> planeTypes = unit.PlaneTypesRepo.GetEntities();
			if (planeTypes == null)
			{
				return null;
			}
			return mapper.Map<List<PlaneType>, List<PlaneTypeDTO>>(planeTypes);
		}

		public StewardessDTO GetStewardessById(int id)
		{
			Stewardess stewardess = unit.StewardessesRepo.GetEntityById(id);
			if (stewardess == null)
			{
				return null;
			}

			return mapper.Map<Stewardess, StewardessDTO>(stewardess);
		}

		public List<StewardessDTO> GetStewardesses()
		{
			List<Stewardess> stewardesses = unit.StewardessesRepo.GetEntities();
			if (stewardesses == null)
			{
				return null;
			}
			return mapper.Map<List<Stewardess>, List<StewardessDTO>>(stewardesses);
		}

		public List<TicketDTO> GetTickets()
		{
			List<Flight> flights = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").FindAll(p => p.Tickets != null && p.Tickets.Count > 0);
			if (flights != null && flights.Count > 0)
			{
				var tickets = new List<Ticket>();
				foreach (var item in flights)
				{
					tickets.AddRange(item.Tickets);
				}
				return mapper.Map<List<Ticket>, List<TicketDTO>>(tickets);
			}
			else
			{
				throw new Exception("Error: There are no tickets.");
			}
		}

		public List<TicketDTO> GetTicketsByFlightId(int flightId)
		{
			List<Ticket> tickets = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").Find(p => p.Id == flightId).Tickets;
			return mapper.Map<List<Ticket>, List<TicketDTO>>(tickets);
		}

		public void UpdateCrew(CrewDTO value)
		{
			if (value != null)
			{
				Crew newCrew = mapper.Map<CrewDTO, Crew>(value);
				unit.DeparturesRepo.GetEntities().FindAll(p => p.CrewItem.Id.Equals(value.Id)).ForEach(c => c.CrewItem = newCrew);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void UpdateDeparture(DepartureDTO departure)
		{
			if (departure != null)
			{
				Departure updatedDepart = mapper.Map<DepartureDTO, Departure>(departure);
				unit.DeparturesRepo.Update(updatedDepart);
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void UpdateFlight(FlightDTO flight)
		{
			if (flight != null)
			{
				Flight insertingFlight = mapper.Map<FlightDTO, Flight>(flight);
				unit.FlightsRepo.Update(insertingFlight);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void UpdatePilot(PilotDTO pilot)
		{
			if (pilot != null)
			{
				Pilot updtPilot = mapper.Map<PilotDTO, Pilot>(pilot);
				unit.PilotsRepo.Update(updtPilot);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void UpdatePlane(PlaneDTO value)
		{
			if (value != null)
			{
				Plane newPlane = mapper.Map<PlaneDTO, Plane>(value);
				var departure = unit.DeparturesRepo.GetEntities(includeProperties: "PlaneItem").Find(p => p.PlaneItem.Id.Equals(value.Id));
				departure.PlaneItem = newPlane;
				unit.DeparturesRepo.Update(departure);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void UpdateStewardess(StewardessDTO stewardess)
		{
			if (stewardess != null)
			{
				Stewardess updtStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess);
				unit.StewardessesRepo.Update(updtStewardess);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void UpdateTicket(TicketDTO value)
		{
			if (value != null)
			{
				Ticket changedTicket = mapper.Map<TicketDTO, Ticket>(value);
				var flight = unit.FlightsRepo.GetEntityById(value.FlightId);
				var tickets = flight.Tickets;
				var ticket = tickets.Find(k => k.Id == value.Id);
				if (ticket != null)
				{
					tickets.Remove(ticket);
					tickets.Add(changedTicket);
					unit.FlightsRepo.Update(flight);
				}
				else
				{
					throw new Exception("Error: There is no such ticket in this flight.");
				}
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void UpdateType(PlaneTypeDTO planeType)
		{
			if (planeType != null)
			{
				PlaneType updtPlaneType = mapper.Map<PlaneTypeDTO, PlaneType>(planeType);
				unit.PlaneTypesRepo.Update(updtPlaneType);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}
	}
}
