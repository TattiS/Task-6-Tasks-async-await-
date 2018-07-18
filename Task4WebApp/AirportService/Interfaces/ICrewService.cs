using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface ICrewService
    {
		void CreateCrew(int departId, CrewDTO value);
		CrewDTO GetCrewById(int id);
		List<CrewDTO> GetCrews();
		List<CrewDTO> GetCrewsBy(Predicate<CrewDTO> predicate);
		void UpdateCrew(CrewDTO value);
		void DeleteCrew(int id);
	}
}
