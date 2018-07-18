using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
    public class AsyncCrewService: IAsyncCrewService
    {
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public AsyncCrewService(AsyncUnitOfWork unitOfWork)
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

				c.CreateMap<Crew, CrewDTO>().ReverseMap();

			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}



		public async Task<CrewDTO> CreateCrew(int departId, CrewDTO value)
		{
			var departure = await unit.DeparturesRepo.GetEntityById(departId);
			if (departure != null)
			{
				var crew = mapper.Map<CrewDTO, Crew>(value) ?? throw new AutoMapperMappingException("Error: Can't map the crewDTO into crew"); 
				var s = await unit.CrewRepo.Insert(crew);
				departure.CrewItem = crew ?? throw new Exception("Error: Can't add this crew to the departure!");
				var d = await unit.DeparturesRepo.Update(departure);
				await unit.SaveChangesAsync();
				return value;
			}
			else
			{
				throw new Exception("Error: Can't find such departure!");
			}
		}

		public async Task<CrewDTO> GetCrewById(int id)
		{
			var crew = await unit.CrewRepo.GetEntityById(id);
			var result = mapper.Map<Crew, CrewDTO>(crew)?? throw new AutoMapperMappingException("Error: Can't map the crew into crewDTO");
			return result;
		}

		public async Task<List<CrewDTO>> GetCrews()
		{
			List<Crew> crews = new List<Crew>();

			crews = await unit.CrewRepo.GetAllEntities();
			var result = mapper.Map<List<Crew>, List<CrewDTO>>(crews)?? throw new AutoMapperMappingException("Error: Can't map the list of crews into the list of crewDTO");
			return result;
		}

		public async Task<List<CrewDTO>> GetCrewsBy(Predicate<CrewDTO> predicate)
		{
			
			var crews = await GetCrews();

			return crews.FindAll(predicate);
		}

		public async Task<CrewDTO> UpdateCrew(CrewDTO value)
		{
			if (value != null)
			{
				Crew newCrew = mapper.Map<CrewDTO, Crew>(value)?? throw new AutoMapperMappingException("Error: Can't map the crewDTO into crew");
				var s = await unit.CrewRepo.Update(newCrew) ?? throw new Exception("Couldn't find such crew in db");
				await unit.SaveChangesAsync();
				return value;
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<int> DeleteCrew(int id)
		{
			var itemToDelete = await unit.CrewRepo.GetEntityById(id);
			if (itemToDelete != null)
			{
				await unit.CrewRepo.Delete(itemToDelete);
				return await unit.SaveChangesAsync();
			}
			else
			{
				throw new Exception("Can't find such crew!");
			}
		}
	}
}
