using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Timers;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
	public class AsyncFlightService : IAsyncFlightService
	{
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public AsyncFlightService(AsyncUnitOfWork unitOfWork)
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

		public async Task<FlightDTO> CreateFlight(FlightDTO flight)
		{
			if (flight == null)
			{
				Flight newFlight = mapper.Map<FlightDTO, Flight>(flight) ?? throw new AutoMapperMappingException("Error: Can't map the flightDTO into flight");

				var result = await unit.FlightsRepo.Insert(newFlight);
				await unit.SaveChangesAsync();
				return mapper.Map<Flight, FlightDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the flight into flightDTO");
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<FlightDTO> GetFlightById(int id)
		{
			var subresult = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets");

			if (subresult == null || subresult.Count <= 0)
			{
				return null;
			}
			var result = subresult.Find(p => p.Id == id);
			return mapper.Map<Flight, FlightDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the flight into flightDTO");
		}

		public async Task<List<FlightDTO>> GetFlights()
		{

			List<Flight> subresult = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets");

			List<FlightDTO> result = null;
			if (subresult != null)
			{
				result = mapper.Map<List<Flight>, List<FlightDTO>>(subresult) ?? throw new AutoMapperMappingException("Error: Can't map the flight into flightDTO");
			}
			return result;
		}

		public async Task<List<FlightDTO>> GetFlightsByArrival(DateTime time)
		{
			List<Flight> subresult = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets", filter: (f => f.ArrivalTime == time));
			if (subresult == null)
			{
				return null;
			}
			return mapper.Map<List<Flight>, List<FlightDTO>>(subresult) ?? throw new AutoMapperMappingException("Error: Can't map the list flight into the list flightDTO");
		}

		public async Task<List<FlightDTO>> GetFlightsByDeparture(DateTime time)
		{
			List<Flight> result = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets", filter: (f => f.DepartureTime == time));
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Flight>, List<FlightDTO>>(result) ?? throw new AutoMapperMappingException("Error: Can't map the list flight into the list flightDTO");
		}

		public async Task<List<FlightDTO>> GetFlightsByDestination(string destination)
		{

			List<Flight> result = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets", filter: (f => f.Destination == destination));
			if (result == null)
			{
				return null;
			}

			return mapper.Map<List<Flight>, List<FlightDTO>>(result) ?? throw new AutoMapperMappingException("Error: Can't map the list flight into the list flightDTO"); 
		}

		public async Task< List<FlightDTO>> GetFlightsByPoint(string departurePoint)
		{
			List<Flight> result = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets", filter:(f => f.DeparturePoint == departurePoint));
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Flight>, List<FlightDTO>>(result) ?? throw new AutoMapperMappingException("Error: Can't map the list flight into the list flightDTO"); 
		}

		public async Task<FlightDTO> UpdateFlight(FlightDTO flight)
		{
			if (flight != null)
			{
				Flight insertingFlight = mapper.Map<FlightDTO, Flight>(flight) ?? throw new AutoMapperMappingException("Error: Can't map the flightDTO into the flight");
				var result = await unit.FlightsRepo.Update(insertingFlight);
				await unit.SaveChangesAsync();
				return mapper.Map<Flight, FlightDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the flight into flightDTO");
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<int> DeleteFlight(int id)
		{
			var flightToDelete = await unit.FlightsRepo.GetEntityById(id);
			if (flightToDelete != null)
			{
				var result = await unit.FlightsRepo.Delete(id);
				await unit.SaveChangesAsync();
				return result;
			}
			else
			{
				throw new Exception("Error: Cant't find such flight to delete.");
			}
		}



		private List<FlightDTO> GetFlightsSync()
		{
			List<FlightDTO> result = null;
			List<Flight> flights = unit.FlightsRepo.GetAll();
			if (flights != null)
			{
				result = mapper.Map<List<Flight>, List<FlightDTO>>(flights) ?? throw new AutoMapperMappingException("Error: Can't map the flight into flightDTO");
			}

			return result;
		}

		public  Task<List<FlightDTO>> RunAsync(int delay = 5000)
		{
			if (GetFlightsSync() == null) throw new ArgumentNullException("TaskHelper");
			var tcs = new TaskCompletionSource<List<FlightDTO>>();

			Timer timer = new Timer(delay);
			
				timer.Start();
				timer.Elapsed += (o, e) =>
				{
					try
					{
						List<FlightDTO> result = GetFlightsSync();
						tcs.SetResult(result);
						timer.Stop();
					}
					catch (Exception exc)
					{
						tcs.SetException(exc);
					}

				};
			
			return tcs.Task;
		}
	}
}
