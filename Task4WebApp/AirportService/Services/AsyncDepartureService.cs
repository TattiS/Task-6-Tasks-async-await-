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
	public class AsyncDepartureService:IAsyncDepartureService
    {
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public AsyncDepartureService(AsyncUnitOfWork unitOfWork)
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
				c.CreateMap<Departure, DepartureDTO>().ReverseMap();

			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public async Task<DepartureDTO> CreateDeparture(DepartureDTO departure)
		{
			if (departure != null)
			{
				Departure newDepart = mapper.Map<DepartureDTO, Departure>(departure) ?? throw new AutoMapperMappingException("Error: Can't map the departureDTO into departure");
				await unit.DeparturesRepo.Insert(newDepart);
				await unit.SaveChangesAsync();
				return departure;
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<DepartureDTO> GetDepartureById(int id)
		{
			Departure departure = await unit.DeparturesRepo.GetEntityById(id);
			if (departure == null)
			{
				throw new ArgumentOutOfRangeException(nameof(departure));
			}
			return mapper.Map<Departure, DepartureDTO>(departure) ?? throw new AutoMapperMappingException("Error: Can't map the departure into departureDTO");
		}

		public async Task<List<DepartureDTO>> GetDepartures()
		{
			List<Departure> result = await unit.DeparturesRepo.GetAllEntities();
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Departure>, List<DepartureDTO>>(result) ?? throw new AutoMapperMappingException("Error: Can't map the departure into departureDTO");
		}

		public async Task<DepartureDTO>  UpdateDeparture(DepartureDTO departure)
		{
			if (departure != null)
			{
				Departure updatedDepart = mapper.Map<DepartureDTO, Departure>(departure) ?? throw new AutoMapperMappingException("Error: Can't map the departureDTO into departure");
				var result = await unit.DeparturesRepo.Update(updatedDepart);
				await unit.SaveChangesAsync();
				return mapper.Map<Departure, DepartureDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the departure into departureDTO");
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<int> DeleteDeparture(int id)
		{

			var departureToDelete = await unit.DeparturesRepo.GetEntityById(id);
			if (departureToDelete != null)
			{
				var result = await unit.DeparturesRepo.Delete(id);
				await unit.SaveChangesAsync();
				return result;
			}
			else
			{
				throw new Exception("Error: Cant't find such departure to delete.");
			}
		}
	}
}
