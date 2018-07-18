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
    public class CrewService:ICrewService
    {
		private static UnitOfWork unit;
		private static IMapper mapper;

		public CrewService(UnitOfWork unitOfWork)
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

		public void CreateCrew(int departId, CrewDTO value)
		{
			var departure = unit.DeparturesRepo.GetEntityById(departId);
			if (departure != null)
			{
				var crew = mapper.Map<CrewDTO, Crew>(value);
				if (crew == null)
				{
					throw new Exception("Error: Can't add this crew to the the departure!");
				}
				departure.CrewItem = crew;
				unit.DeparturesRepo.Update(departure);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Can't find such departure!");
			}
		}

		public CrewDTO GetCrewById(int id)
		{
			return GetCrews()?.Find(p => p.Id == id);
		}

		public List<CrewDTO> GetCrews()
		{
			List<Crew> crews = new List<Crew>();

			crews = unit.CrewRepository.GetEntities();

			return mapper.Map<List<Crew>, List<CrewDTO>>(crews);
		}

		public List<CrewDTO> GetCrewsBy(Predicate<CrewDTO> predicate)
		{
			return GetCrews()?.FindAll(predicate);
		}

		public void UpdateCrew(CrewDTO value)
		{
			if (value != null)
			{
				Crew newCrew = mapper.Map<CrewDTO, Crew>(value);
				unit.CrewRepository.Update(newCrew);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void DeleteCrew(int id)
		{
			var itemToDelete = unit.DeparturesRepo.GetEntities().Find(p => p.CrewItem.Id == id);
			if (itemToDelete != null)
			{
				itemToDelete.CrewItem = null;
				unit.DeparturesRepo.Update(itemToDelete);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Can't find such crew!");
			}
		}

	}
}
