using System;
using System.Collections.Generic;
using System.Text;
using DALProject.Models;

namespace DTOLibrary.ApiDTOs
{
    public class ApiPilot 
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public long Exp { get; set; }
		public int CrewId { get; set; }
	}
}
