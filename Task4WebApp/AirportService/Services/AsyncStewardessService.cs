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
	public class AsyncStewardessService:IAsyncStewardessService
    {
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public AsyncStewardessService(AsyncUnitOfWork unitOfWork)
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
				
				c.CreateMap<Stewardess, StewardessDTO>().ReverseMap();
				
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public async Task<StewardessDTO> CreateStewardess(StewardessDTO stewardess)
		{
			if (stewardess != null)
			{
				Stewardess newStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess) ?? throw new AutoMapperMappingException("Error: Can't map the stewardessDTO into stewardess");
				var result = await unit.StewardessesRepo.Insert(newStewardess);
				await unit.SaveChangesAsync();
				return mapper.Map<Stewardess,StewardessDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the stewardess into stewardessDTO");
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<StewardessDTO> GetStewardessById(int id)
		{
			Stewardess stewardess = await unit.StewardessesRepo.GetEntityById(id);
			if (stewardess == null)
			{
				return null;
			}

			return mapper.Map<Stewardess, StewardessDTO>(stewardess) ?? throw new AutoMapperMappingException("Error: Can't map the stewardess into stewardessDTO");
		}

		public async Task<List<StewardessDTO>> GetStewardesses()
		{
			List<Stewardess> stewardesses = await unit.StewardessesRepo.GetAllEntities();
			if (stewardesses == null)
			{
				return null;
			}
			return mapper.Map<List<Stewardess>, List<StewardessDTO>>(stewardesses) ?? throw new AutoMapperMappingException("Error: Can't map the stewardess into stewardessDTO");
		}

		public async Task<StewardessDTO> UpdateStewardess(StewardessDTO stewardess)
		{
			if (stewardess != null)
			{
				Stewardess updtStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess) ?? throw new AutoMapperMappingException("Error: Can't map the stewardessDTO into stewardess");
				var result = await unit.StewardessesRepo.Update(updtStewardess);
				await unit.SaveChangesAsync();
				return mapper.Map<Stewardess, StewardessDTO>(result) ?? throw new AutoMapperMappingException("Error: Can't map the stewardess into stewardessDTO"); ;
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<int> DeleteStewardess(int id)
		{
			var stewardessToDelete = await unit.StewardessesRepo.GetEntityById(id);
			if (stewardessToDelete != null)
			{
				var result = await unit.StewardessesRepo.Delete(id);
				await unit.SaveChangesAsync();
				return result;
			}
			else
			{
				throw new Exception("Error: Cant't find such stewardess to delete.");
			}
		}



	}
}
