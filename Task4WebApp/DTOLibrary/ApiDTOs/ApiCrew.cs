using System.Collections.Generic;

namespace DTOLibrary.ApiDTOs
{
	public class ApiCrew 
    {
		public int Id { get; set; }
		public List<ApiPilot> Pilot { get; set; }
		public List<ApiStewardess> Stewardess { get; set; }
	}
}
