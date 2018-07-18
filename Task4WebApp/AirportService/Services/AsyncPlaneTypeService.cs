using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
	public class AsyncPlaneTypeService:IAsyncPlaneTypeService
    {
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public AsyncPlaneTypeService(AsyncUnitOfWork unitOfWork)
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

		public async Task<PlaneTypeDTO> CreatePlaneType(PlaneTypeDTO planeType)
		{
			if (planeType != null)
			{
				PlaneType newPlaneType = mapper.Map<PlaneTypeDTO, PlaneType>(planeType) ?? throw new AutoMapperMappingException("Error: Can't map the PlaneTypeDTO into PlaneType"); ;
				var result = await unit.PlaneTypesRepo.Insert(newPlaneType);
				await unit.SaveChangesAsync();
				return mapper.Map<PlaneType,PlaneTypeDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the planeType into planeTypeDTO");
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<PlaneTypeDTO> GetPlaneTypeById(int id)
		{
			PlaneType type = await unit.PlaneTypesRepo.GetEntityById(id);
			if (type == null)
			{
				return null;
			}
			return mapper.Map<PlaneType, PlaneTypeDTO>(type) ?? throw new AutoMapperMappingException("Error: Can't map the planeType into planeTypeDTO");
		}

		public async Task<List<PlaneTypeDTO>> GetPlaneTypes()
		{
			List<PlaneType> planeTypes = await unit.PlaneTypesRepo.GetAllEntities();
			if (planeTypes == null)
			{
				return null;
			}
			return mapper.Map<List<PlaneType>, List<PlaneTypeDTO>>(planeTypes) ?? throw new AutoMapperMappingException("Error: Can't map the planeType into planeTypeDTO");
		}

		public async Task<PlaneTypeDTO> UpdateType(PlaneTypeDTO planeType)
		{
			if (planeType != null)
			{
				PlaneType updtPlaneType = mapper.Map<PlaneTypeDTO, PlaneType>(planeType) ?? throw new AutoMapperMappingException("Error: Can't map the planeTypeDTO into planeType");
				var result = await unit.PlaneTypesRepo.Update(updtPlaneType);
				await unit.SaveChangesAsync();
				return mapper.Map<PlaneType, PlaneTypeDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the planeType into planeTypeDTO");

			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<int> DeletePlaneType(int id)
		{
			var typeToDelete = await unit.PlaneTypesRepo.GetEntityById(id);
			if (typeToDelete != null)
			{
				var result = await unit.PlaneTypesRepo.Delete(id);
				await unit.SaveChangesAsync();
				return result;
			}
			else
			{
				throw new Exception("Error: Cant't find such type of plane to delete.");
			}
		}

	}
}
