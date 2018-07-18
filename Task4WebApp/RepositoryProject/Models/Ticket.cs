using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALProject.Models
{
	public class Ticket: BaseEntity
	{
		[Required]
		public double Price { get; set; }
		public int FlightId { get; set; }
		
	}
}
