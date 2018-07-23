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
	public class PilotService : IPilotService
    {
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public PilotService(AsyncUnitOfWork unitOfWork)
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

		public async Task<PilotDTO> CreatePilot(PilotDTO pilot)
		{
			if (pilot != null)
			{
				if (await unit.PilotsRepo.GetEntityById(pilot.Id) != null)
				{
					throw new ArgumentOutOfRangeException("Such user exsists!");
				}
				Pilot newPilot = mapper.Map<PilotDTO, Pilot>(pilot);
				if (newPilot == null || newPilot.Name == null || newPilot.Surname ==null)
				{
					throw new AutoMapperMappingException("Couldn't map PilotDTO into Pilot");
				}

				var result = await unit.PilotsRepo.Insert(newPilot);
				await unit.SaveChangesAsync();
				return mapper.Map<Pilot,PilotDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the pilot into pilotDTO");
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<PilotDTO> GetPilotById(int id)
		{
			Pilot pilot = await unit.PilotsRepo.GetEntityById(id);
			if (pilot == null)
			{
				throw new ArgumentOutOfRangeException(nameof(pilot));
			}
			return mapper.Map<Pilot, PilotDTO>(pilot) ?? throw new AutoMapperMappingException("Error: Can't map the pilot into pilotDTO");
		}

		public async Task<List<PilotDTO>> GetPilots()
		{
			List<Pilot> pilots = await unit.PilotsRepo.GetAllEntities();
			if (pilots == null)
			{
				return null;
			}
			return mapper.Map<List<Pilot>, List<PilotDTO>>(pilots) ?? throw new AutoMapperMappingException("Error: Can't map the pilot list into pilotDTO list"); 
		}

		public async Task<PilotDTO> UpdatePilot(PilotDTO pilot)
		{
			if (pilot != null)
			{
				
				if (await unit.PilotsRepo.GetEntityById(pilot.Id) == null)
				{
					throw new ArgumentOutOfRangeException("Such user doesn't exsist!");
				}
				Pilot updtPilot = mapper.Map<PilotDTO, Pilot>(pilot);
				if (updtPilot == null || updtPilot.Name == null || updtPilot.Surname == null)
				{
					throw new AutoMapperMappingException("Couldn't map PilotDTO into Pilot");
				}
				var result = await unit.PilotsRepo.Update(updtPilot);
				await unit.SaveChangesAsync();
				return mapper.Map<Pilot,PilotDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the pilot into pilotDTO");
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<int> DeletePilot(int id)
		{
			var pilotToDelete = await unit.PilotsRepo.GetEntityById(id);
			if (pilotToDelete != null)
			{
				var result = await unit.PilotsRepo.Delete(id);
				await unit.SaveChangesAsync();
				return result;
			}
			else
			{
				throw new Exception("Error: Cant't find such pilot to delete.");
			}
		}

		
	}
}
