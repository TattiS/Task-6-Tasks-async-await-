using System.Collections.Generic;
using System.Threading.Tasks;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	interface IAsyncPilotService
    {
		Task<PilotDTO> CreatePilot(PilotDTO pilot);
		Task<PilotDTO> GetPilotById(int id);
		Task<List<PilotDTO>> GetPilots();
		Task<PilotDTO> UpdatePilot(PilotDTO pilot);
		Task<int> DeletePilot(int id);
	}
}
