using System.ComponentModel.DataAnnotations;

namespace DTOLibrary.DTOs
{
	public class PlaneTypeDTO
    {
		public int Id { get; set; }
		[Required]
		public string Model { get; set; }
		[Required]
		public int Seats { get; set; }
		[Required]
		public int AirLift { get; set; }

	}
}
