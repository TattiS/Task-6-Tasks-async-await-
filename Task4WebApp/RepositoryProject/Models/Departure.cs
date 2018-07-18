using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DALProject.Models
{
	public class Departure : BaseEntity
	{
		[Required]
		public int FlightId { get; set; }
		[Required]
		public DateTime DepartureDate { get; set; }
		public int CrewItemId { get; set; }
		[ForeignKey("CrewItemId")]
		public Crew CrewItem { get; set; }
		public int PlaneItemId { get; set; }
		[ForeignKey("PlaneItemId")]
		public Plane PlaneItem { get; set; }
		public Flight Flight { get; set; }
	}
}
