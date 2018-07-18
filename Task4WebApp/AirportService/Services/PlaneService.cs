using System;
using System.Collections.Generic;
using System.Text;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
    public class PlaneService:IPlaneService
    {
		private static UnitOfWork unit;
		private static IMapper mapper;

		public PlaneService(UnitOfWork unitOfWork)
		{
			unit = unitOfWork;
			if (mapper == null)
			{
				ConfigureMapper();
			}

		}
		private void ConfigureMapper()
		{
			var mapConfig = new MapperConfiguration(c =>
			{
				
				c.CreateMap<Plane, PlaneDTO>().ForMember(e => e.ExpiryDate, opt => opt.Ignore());
				c.CreateMap<PlaneDTO, Plane>().ForMember(e => e.OperationLife, opt => opt.MapFrom(src => (src.ExpiryDate.Subtract(src.ReleaseDate))))
											  .ForMember(e => e.TimeTicks, opt => opt.Ignore())
											  .ForMember(e => e.TypeId, opt => opt.Ignore()); ;
				
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public void CreatePlane(int departId, PlaneDTO value)
		{
			var departure = unit.DeparturesRepo.GetEntityById(departId);
			if (departure != null)
			{
				var plane = mapper.Map<PlaneDTO, Plane>(value);
				if (plane == null)
				{
					throw new Exception("Error: Can't add this crew to the the departure!");
				}
				departure.PlaneItem = plane;
				unit.DeparturesRepo.Update(departure);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Can't find such departure!");
			}
		}

		public PlaneDTO GetPlaneById(int id)
		{
			return GetPlanes().Find(p => p.Id == id);
		}

		public List<PlaneDTO> GetPlanes()
		{
			var departures = unit.DeparturesRepo.GetEntities(includeProperties: "CrewItem,PlaneItem").FindAll(p => p.PlaneItem != null);
			List<Plane> planes = new List<Plane>();
			if (departures != null && departures.Count > 0)
			{
				foreach (var item in departures)
				{
					planes.Add(item.PlaneItem);
				}
				return mapper.Map<List<Plane>, List<PlaneDTO>>(planes);
			}
			else
			{
				throw new Exception("Error: There isn't any plane.");
			}
		}

		public void UpdatePlane(PlaneDTO value)
		{
			if (value != null)
			{
				Plane newPlane = mapper.Map<PlaneDTO, Plane>(value);
				var departure = unit.DeparturesRepo.GetEntities(includeProperties: "PlaneItem").Find(p => p.PlaneItem.Id.Equals(value.Id));
				departure.PlaneItem = newPlane;
				unit.DeparturesRepo.Update(departure);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void DeletePlane(int id)
		{
			var itemToDelete = unit.DeparturesRepo.GetEntities().Find(p => p.PlaneItem.Id == id);
			if (itemToDelete != null)
			{
				itemToDelete.PlaneItem = null;
				unit.DeparturesRepo.Update(itemToDelete);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Can't find such plane to delete!");
			}
		}

	}
}
