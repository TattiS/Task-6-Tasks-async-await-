using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface IStewardessService
    {
		void CreateStewardess(StewardessDTO stewardess);
		StewardessDTO GetStewardessById(int id);
		List<StewardessDTO> GetStewardesses();
		void UpdateStewardess(StewardessDTO stewardess);
		void DeleteStewardess(int id);
	}
}
