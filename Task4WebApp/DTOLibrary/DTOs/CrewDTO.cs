using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOLibrary.DTOs
{
    public class CrewDTO
    {
		public int Id { get; set; }
		[Required]
		public int PilotId { get; set; }
		public List<StewardessDTO> Stewardesses { get; set; }

	}
}
