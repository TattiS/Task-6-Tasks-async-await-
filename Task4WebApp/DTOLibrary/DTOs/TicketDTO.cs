using System.ComponentModel.DataAnnotations;

namespace DTOLibrary.DTOs
{
	public class TicketDTO
    {
		public int Id { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public int FlightId { get; set; }

	}
}
