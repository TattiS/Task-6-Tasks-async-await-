using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface IAsyncStewardessService
    {
		Task<StewardessDTO> CreateStewardess(StewardessDTO stewardess);
		Task<StewardessDTO> GetStewardessById(int id);
		Task<List<StewardessDTO>> GetStewardesses();
		Task<StewardessDTO> UpdateStewardess(StewardessDTO stewardess);
		Task<int> DeleteStewardess(int id);
	}
}
