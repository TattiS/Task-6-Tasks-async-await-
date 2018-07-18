using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface IPilotService
    {
		void CreatePilot(PilotDTO pilot);
		PilotDTO GetPilotById(int id);
		List<PilotDTO> GetPilots();
		void UpdatePilot(PilotDTO pilot);
		void DeletePilot(int id);
	}
}
