using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DALProject.Models;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
	public interface IAsyncCrewService
	{
		Task<CrewDTO> CreateCrew(int departId, CrewDTO value);
		Task<CrewDTO> GetCrewById(int id);
		Task<List<CrewDTO>> GetCrews();
		Task<List<CrewDTO>> GetCrewsBy(Predicate<CrewDTO> predicate);
		Task<CrewDTO> UpdateCrew(CrewDTO value);
		Task<int> DeleteCrew(int id);
	}
}
