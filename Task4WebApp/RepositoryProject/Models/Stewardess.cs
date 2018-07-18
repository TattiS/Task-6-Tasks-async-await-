using System;
using System.ComponentModel.DataAnnotations;

namespace DALProject.Models
{
	public class Stewardess : BaseEntity
	{
		[StringLength(50)]
		[Required]
		public string Name { get; set; }
		[StringLength(50)]
		[Required]
		public string Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public int CrewId { get; set; }
	}
}
