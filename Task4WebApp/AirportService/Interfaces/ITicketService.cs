using System.Collections.Generic;
using System.Threading.Tasks;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	interface ITicketService
    {
		Task<TicketDTO> CreateTicket(int flightId, TicketDTO value);
		Task<List<TicketDTO>> GetTickets();
		Task<List<TicketDTO>> GetTicketsByFlightId(int flightId);
		Task<List<TicketDTO>> UpdateTicket(TicketDTO value);
		Task<int> DeleteTicket(int id);
	}
}
