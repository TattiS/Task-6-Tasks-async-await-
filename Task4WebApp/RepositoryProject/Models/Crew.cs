using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALProject.Models
{
	public class Crew:BaseEntity
    {
		
		public int PilotId { get; set; }
	
		public List<Stewardess> Stewardesses { get; set; }
		
	}
}
