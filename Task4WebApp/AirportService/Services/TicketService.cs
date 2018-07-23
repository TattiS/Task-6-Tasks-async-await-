using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
	public class TicketService:ITicketService
    {
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public TicketService(AsyncUnitOfWork unitOfWork)
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
				c.CreateMap<Ticket, TicketDTO>().ReverseMap();
				
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public async Task<TicketDTO> CreateTicket(int flightId, TicketDTO value)
		{
			var flight = await unit.FlightsRepo.GetEntityById(flightId);
			if (flight != null)
			{
				var ticket = mapper.Map<TicketDTO, Ticket>(value) ?? throw new AutoMapperMappingException("Error: Can't map the TicketDTO into Ticket");
				if (flight.Tickets.Contains(ticket))
				{
					throw new Exception("Error: Can't add this ticket to the the flight!");
				}
				flight.Tickets.Add(ticket);
				var result = await unit.FlightsRepo.Update(flight);
				await unit.SaveChangesAsync();
				return mapper.Map<Ticket,TicketDTO>(ticket) ?? throw new AutoMapperMappingException("Error: Can't map the Ticket into TicketDTO");
			}
			else
			{
				throw new Exception("Error: Can't find such flight!");
			}
		}

		public async Task<List<TicketDTO>> GetTickets()
		{
			List<Flight> flights = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets", filter:(p => p.Tickets != null && p.Tickets.Count > 0));
			if (flights != null && flights.Count > 0)
			{
				var tickets = new List<Ticket>();
				foreach (var item in flights)
				{
					tickets.AddRange(item.Tickets);
				}
				return mapper.Map<List<Ticket>, List<TicketDTO>>(tickets) ?? throw new AutoMapperMappingException("Error: Can't map the Ticket into TicketDTO");
			}
			else
			{
				throw new Exception("Error: There are no tickets.");
			}
		}

		public async Task <List<TicketDTO>> GetTicketsByFlightId(int flightId)
		{
			var flights = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets", filter:(p => p.Id == flightId));
			var tickets = flights.Find(p => p.Id==flightId).Tickets;
			if (tickets == null && tickets.Count <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(tickets));
			}
			return mapper.Map<List<Ticket>, List<TicketDTO>>(tickets) ?? throw new AutoMapperMappingException("Error: Can't map the Ticket into TicketDTO");
		}

		public async Task<List<TicketDTO>> UpdateTicket(TicketDTO value)
		{
			if (value != null)
			{
				Ticket changedTicket = mapper.Map<TicketDTO, Ticket>(value) ?? throw new AutoMapperMappingException("Error: Can't map the TicketDTO into Ticket");
				var flight = await unit.FlightsRepo.GetEntityById(value.FlightId);
				var tickets = flight.Tickets;
				var ticket = tickets.Find(k => k.Id == value.Id);
				if (ticket != null)
				{
					tickets.Remove(ticket);
					tickets.Add(changedTicket);
					var result = await unit.FlightsRepo.Update(flight);
					await unit.SaveChangesAsync();
					return mapper.Map<List<Ticket>, List<TicketDTO>>(result.Tickets) ?? throw new AutoMapperMappingException("Error: Can't map the Ticket into TicketDTO");
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

		public async Task<int> DeleteTicket(int id)
		{

			var flights = await unit.FlightsRepo.GetEntities(includeProperties: "Tickets", filter:(p => p.Tickets.Find(i => i.Id == id) != null));
			if (flights != null)
			{
				var flight = flights.Find(p => p.Tickets.Exists(t => t.Id == id));
				flight?.Tickets.RemoveAll(p => p.Id == id);
				await unit.FlightsRepo.Update(flight);
				return await unit.SaveChangesAsync();
				
			}
			else
			{
				throw new Exception("Can't find such ticket!");
			}
		}

	}
}
