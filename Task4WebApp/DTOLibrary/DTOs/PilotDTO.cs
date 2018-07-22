﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DTOLibrary.DTOs
{
	public class PilotDTO
    {
		public int Id { get; set; }
		[StringLength(50)]
		[Required]
		public string Name { get; set; }
		[StringLength(50)]
		[Required]
		public string Surname { get; set; }

		public DateTime BirthDate { get; set; }
		public TimeSpan Experience { get; set; }
		public DateTime StartedIn { get; set; }
	}
}
