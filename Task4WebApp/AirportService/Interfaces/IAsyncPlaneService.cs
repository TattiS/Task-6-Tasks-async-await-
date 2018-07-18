using System.Collections.Generic;
using System.Threading.Tasks;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	interface IAsyncPlaneService
    {
		Task<PlaneDTO> CreatePlane(int departId, PlaneDTO value);
		Task<PlaneDTO> GetPlaneById(int id);
		Task<List<PlaneDTO>> GetPlanes();
		Task<PlaneDTO> UpdatePlane(PlaneDTO value);
		Task<int> DeletePlane(int id);
	}
}
