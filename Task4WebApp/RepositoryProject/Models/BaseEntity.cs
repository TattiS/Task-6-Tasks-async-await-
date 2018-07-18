using System.ComponentModel.DataAnnotations;

namespace DALProject.Models
{
	public class BaseEntity
    {
		[Key]
		public int Id { get; set; }
    }
}