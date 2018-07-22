using System;
using System.Collections.Generic;
using System.Text;
using DALProject.Models;

namespace DTOLibrary.ApiDTOs
{
    public class ApiCrew 
    {
		public int Id { get; set; }
		public List<ApiPilot> Pilot { get; set; }
		public List<ApiStewardess> Stewardess { get; set; }
	}
}
