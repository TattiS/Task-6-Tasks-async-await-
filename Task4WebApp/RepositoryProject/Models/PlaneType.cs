using System.ComponentModel.DataAnnotations;

namespace DALProject.Models
{
	public class PlaneType: BaseEntity
	{
		[MaxLength(100)]
		[Required]
		public string Model { get; set; }
		[Required]
		public int Seats { get; set; }
		[Required]
		public int AirLift { get; set; }

	}
}
