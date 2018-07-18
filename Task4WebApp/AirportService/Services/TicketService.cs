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
    public class TicketService:ITicketService
    {
		private static UnitOfWork unit;
		private static IMapper mapper;

		public TicketService(UnitOfWork unitOfWork)
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

	}
}
