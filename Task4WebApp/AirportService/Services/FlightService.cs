using System;
using System.Collections.Generic;
using System.Text;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
    public class FlightService:IFlightService
    {
		private static UnitOfWork unit;
		private static IMapper mapper;

		public FlightService(UnitOfWork unitOfWork)
		{
			unit = unitOfWork;
			if (mapper == null)
			{
				ConfigureMapper();
			}

		}
		private void ConfigureMapper()
		{
			var mapConfig = new MapperConfiguration(c =>
			{
				c.CreateMap<Flight, FlightDTO>().ReverseMap();
				
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
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
			List<Flight> result = unit.FlightsRepo.GetEntities(includeProperties: "Tickets").FindAll(f => f.DeparturePoint == departurePoint);
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Flight>, List<FlightDTO>>(result);
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

	}
}
