using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface ITicketService
    {
		void CreateTicket(int flightId, TicketDTO value);
		List<TicketDTO> GetTickets();
		List<TicketDTO> GetTicketsByFlightId(int flightId);
		void UpdateTicket(TicketDTO value);
		void DeleteTicket(int id);
	}
}
