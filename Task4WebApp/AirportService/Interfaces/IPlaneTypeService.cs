using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface IPlaneTypeService
	{
		void CreatePlaneType(PlaneTypeDTO planeType);
		PlaneTypeDTO GetPlaneTypeById(int id);
		List<PlaneTypeDTO> GetPlaneTypes();
		void UpdateType(PlaneTypeDTO planeType);
		void DeletePlaneType(int id);
	}
}
