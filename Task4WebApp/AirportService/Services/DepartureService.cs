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
    public class DepartureService:IDepartureService
    {
		private static UnitOfWork unit;
		private static IMapper mapper;

		public DepartureService(UnitOfWork unitOfWork)
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

		public void CreateDeparture(DepartureDTO departure)
		{
			if (departure != null)
			{
				Departure newDepart = mapper.Map<DepartureDTO, Departure>(departure);
				unit.DeparturesRepo.Insert(newDepart);
				unit.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public DepartureDTO GetDepartureById(int id)
		{
			Departure departure = unit.DeparturesRepo.GetEntityById(id);
			if (departure == null)
			{
				return null;
			}
			return mapper.Map<Departure, DepartureDTO>(departure);
		}

		public List<DepartureDTO> GetDepartures()
		{
			List<Departure> result = unit.DeparturesRepo.GetEntities(includeProperties: "CrewItem,PlaneItem");
			if (result == null)
			{
				return null;
			}
			return mapper.Map<List<Departure>, List<DepartureDTO>>(result);
		}

		public void UpdateDeparture(DepartureDTO departure)
		{
			if (departure != null)
			{
				Departure updatedDepart = mapper.Map<DepartureDTO, Departure>(departure);
				unit.DeparturesRepo.Update(updatedDepart);
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public void DeleteDeparture(int id)
		{

			var departureToDelete = unit.DeparturesRepo.GetEntityById(id);
			if (departureToDelete != null)
			{
				unit.DeparturesRepo.Delete(id);
				unit.SaveChanges();
			}
			else
			{
				throw new Exception("Error: Cant't find such departure to delete.");
			}
		}


	}
}
