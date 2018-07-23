﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DTOLibrary.DTOs
{
	public class StewardessDTO
    {
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public int CrewId { get; set; }

	}
}
