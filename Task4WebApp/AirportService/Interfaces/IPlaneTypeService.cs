using System.Collections.Generic;
using System.Threading.Tasks;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	interface IPlaneTypeService
	{
		Task<PlaneTypeDTO> CreatePlaneType(PlaneTypeDTO planeType);
		Task<PlaneTypeDTO> GetPlaneTypeById(int id);
		Task<List<PlaneTypeDTO>> GetPlaneTypes();
		Task<PlaneTypeDTO> UpdateType(PlaneTypeDTO planeType);
		Task<int> DeletePlaneType(int id);
	}
}
