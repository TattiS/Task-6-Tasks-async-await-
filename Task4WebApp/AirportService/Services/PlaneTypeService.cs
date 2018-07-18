using System;
using System.Collections.Generic;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
	public class PlaneTypeService:IPlaneTypeService
    {
		private static UnitOfWork unit;
		private static IMapper mapper;

		public PlaneTypeService(UnitOfWork unitOfWork)
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
				
				c.CreateMap<PlaneType, PlaneTypeDTO>().ReverseMap();
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public void CreatePlaneType(PlaneTypeDTO planeType)
		{
			if (planeType != null)
			{
				PlaneType newPlaneType = mapper.Map<PlaneTypeDTO, PlaneType>(planeType);
				unit.PlaneTypesRepo.Insert(newPlaneType);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public PlaneTypeDTO GetPlaneTypeById(int id)
		{
			PlaneType type = unit.PlaneTypesRepo.GetEntityById(id);
			if (type == null)
			{
				return null;
			}
			return mapper.Map<PlaneType, PlaneTypeDTO>(type);
		}

		public List<PlaneTypeDTO> GetPlaneTypes()
		{
			List<PlaneType> planeTypes = unit.PlaneTypesRepo.GetEntities();
			if (planeTypes == null)
			{
				return null;
			}
			return mapper.Map<List<PlaneType>, List<PlaneTypeDTO>>(planeTypes);
		}

		public void UpdateType(PlaneTypeDTO planeType)
		{
			if (planeType != null)
			{
				PlaneType updtPlaneType = mapper.Map<PlaneTypeDTO, PlaneType>(planeType);
				unit.PlaneTypesRepo.Update(updtPlaneType);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void DeletePlaneType(int id)
		{
			var typeToDelete = unit.PlaneTypesRepo.GetEntityById(id);
			if (typeToDelete != null)
			{
				unit.PlaneTypesRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such type of plane to delete.");
			}
		}

	}
}
