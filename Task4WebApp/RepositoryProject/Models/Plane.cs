using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DALProject.Models
{
	public class Plane: BaseEntity
	{
		[MaxLength(50)]
		public string Name { get; set; }
		public int TypeId { get; set; }
		[ForeignKey("TypeId")]
		public PlaneType TypeOfPlane { get; set; }
		public DateTime ReleaseDate { get; set; }
		public long TimeTicks { get; set; }
		[NotMapped]
		public TimeSpan OperationLife
		{
			get
			{
				return new TimeSpan(TimeTicks);
			}
			set
			{
				TimeTicks = value.Ticks;
			}
		}
	}
}
