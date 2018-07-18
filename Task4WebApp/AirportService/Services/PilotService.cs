using System;
using System.Collections.Generic;
using AirportService.Interfaces;
using AutoMapper;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.DTOs;

namespace AirportService.Services
{
	public class PilotService:IPilotService
    {
		private static IUnitOfWork unit;
		private static IMapper mapper;

		public PilotService(IUnitOfWork unitOfWork)
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

				c.CreateMap<Pilot, PilotDTO>().ForMember(e => e.StartedIn, opt => opt.Ignore())
												.ForMember(e => e.Name, opt => opt.PreCondition(src => (src.Name.Length < 50)))
												.ForMember(e => e.Surname, opt => opt.PreCondition(src => (src.Surname.Length < 50)));
				c.CreateMap<PilotDTO, Pilot>().ForMember(e => e.Experience, opt => opt.MapFrom(src => (DateTime.Today.Subtract(src.StartedIn))))
											  .ForMember(e => e.TimeTicks, opt => opt.Ignore())
											  .ForMember(e => e.Name, opt => opt.PreCondition(src => (src.Name.Length < 50)))
											  .ForMember(e => e.Surname, opt => opt.PreCondition(src => (src.Surname.Length < 50)));  
				
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public void CreatePilot(PilotDTO pilot)
		{
			if (pilot != null)
			{
				if (unit.PilotsRepo.GetEntityById(pilot.Id) != null)
				{
					throw new ArgumentOutOfRangeException("Such user exsists!");
				}
				Pilot newPilot = mapper.Map<PilotDTO, Pilot>(pilot);
				if (newPilot == null || newPilot.Name == null || newPilot.Surname ==null)
				{
					throw new AutoMapperMappingException("Couldn't map PilotDTO into Pilot");
				}

				unit.PilotsRepo.Insert(newPilot);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public PilotDTO GetPilotById(int id)
		{
			Pilot pilot = unit.PilotsRepo.GetEntityById(id);
			if (pilot == null)
			{
				return null;
			}
			return mapper.Map<Pilot, PilotDTO>(pilot);
		}

		public List<PilotDTO> GetPilots()
		{
			List<Pilot> pilots = unit.PilotsRepo.GetEntities();
			if (pilots == null)
			{
				return null;
			}
			return mapper.Map<List<Pilot>, List<PilotDTO>>(pilots);
		}

		public void UpdatePilot(PilotDTO pilot)
		{
			if (pilot != null)
			{
				
				if (unit.PilotsRepo.GetEntityById(pilot.Id) == null)
				{
					throw new ArgumentOutOfRangeException("Such user doesn't exsist!");
				}
				Pilot updtPilot = mapper.Map<PilotDTO, Pilot>(pilot);
				if (updtPilot == null || updtPilot.Name == null || updtPilot.Surname == null)
				{
					throw new AutoMapperMappingException("Couldn't map PilotDTO into Pilot");
				}
				unit.PilotsRepo.Update(updtPilot);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void DeletePilot(int id)
		{
			var pilotToDelete = unit.PilotsRepo.GetEntityById(id);
			if (pilotToDelete != null)
			{
				unit.PilotsRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such pilot to delete.");
			}
		}

		
	}
}
