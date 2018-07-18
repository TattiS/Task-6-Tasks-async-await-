using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALProject.Models
{
	public class Flight: BaseEntity
	{
		[StringLength(150)]
		[Required]
		public string DeparturePoint { get; set; }
		[Required]
		public DateTime DepartureTime { get; set; }
		[StringLength(150)]
		[Required]
		public string Destination { get; set; }
		[Required]
		public DateTime ArrivalTime { get; set; }
		public List<Ticket> Tickets { get; set; }

	}
}
