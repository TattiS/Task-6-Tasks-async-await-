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
    public class StewardessService:IStewardessService
    {
		private static UnitOfWork unit;
		private static IMapper mapper;

		public StewardessService(UnitOfWork unitOfWork)
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

		public void CreateStewardess(StewardessDTO stewardess)
		{
			if (stewardess != null)
			{
				Stewardess newStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess);
				unit.StewardessesRepo.Insert(newStewardess);
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public StewardessDTO GetStewardessById(int id)
		{
			Stewardess stewardess = unit.StewardessesRepo.GetEntityById(id);
			if (stewardess == null)
			{
				return null;
			}

			return mapper.Map<Stewardess, StewardessDTO>(stewardess);
		}

		public List<StewardessDTO> GetStewardesses()
		{
			List<Stewardess> stewardesses = unit.StewardessesRepo.GetEntities();
			if (stewardesses == null)
			{
				return null;
			}
			return mapper.Map<List<Stewardess>, List<StewardessDTO>>(stewardesses);
		}

		public void UpdateStewardess(StewardessDTO stewardess)
		{
			if (stewardess != null)
			{
				Stewardess updtStewardess = mapper.Map<StewardessDTO, Stewardess>(stewardess);
				unit.StewardessesRepo.Update(updtStewardess);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void DeleteStewardess(int id)
		{
			var stewardessToDelete = unit.StewardessesRepo.GetEntityById(id);
			if (stewardessToDelete != null)
			{
				unit.StewardessesRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such stewardess to delete.");
			}
		}



	}
}
