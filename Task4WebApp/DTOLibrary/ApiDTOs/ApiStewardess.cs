using System;
using System.ComponentModel.DataAnnotations;
using DALProject.Models;

namespace DTOLibrary.ApiDTOs
{
	public class ApiStewardess 
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public int CrewId { get; set; }
	}
}
