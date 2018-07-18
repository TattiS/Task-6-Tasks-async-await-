using System;
using System.Collections.Generic;
using System.Text;
using DTOLibrary.DTOs;

namespace AirportService.Interfaces
{
    interface IPlaneService
    {
		void CreatePlane(int departId, PlaneDTO value);
		PlaneDTO GetPlaneById(int id);
		List<PlaneDTO> GetPlanes();
		void UpdatePlane(PlaneDTO value);
		void DeletePlane(int id);
	}
}
