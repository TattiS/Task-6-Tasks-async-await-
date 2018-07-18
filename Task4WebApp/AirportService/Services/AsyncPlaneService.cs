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
    public class AsyncPlaneService:IAsyncPlaneService
    {
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public AsyncPlaneService(AsyncUnitOfWork unitOfWork)
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
				
				c.CreateMap<Plane, PlaneDTO>().ForMember(e => e.ExpiryDate, opt => opt.Ignore());
				c.CreateMap<PlaneDTO, Plane>().ForMember(e => e.OperationLife, opt => opt.MapFrom(src => (src.ExpiryDate.Subtract(src.ReleaseDate))))
											  .ForMember(e => e.TimeTicks, opt => opt.Ignore())
											  .ForMember(e => e.TypeId, opt => opt.Ignore()); ;
				
			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public async Task<PlaneDTO> CreatePlane(int departId, PlaneDTO value)
		{
			var departure = await unit.DeparturesRepo.GetEntityById(departId);
			if (departure != null)
			{
				var plane = mapper.Map<PlaneDTO, Plane>(value) ?? throw new AutoMapperMappingException("Error: Can't map the planeDTO into plane"); 
				
				departure.PlaneItem = plane;
				var result = await unit.DeparturesRepo.Update(departure);
				await unit.SaveChangesAsync();
				return mapper.Map<Plane, PlaneDTO>(result.PlaneItem) ?? throw new AutoMapperMappingException("Error: Can't map the plane into planeDTO");
			}
			else
			{
				throw new Exception("Error: Can't find such departure!");
			}
		}

		public async Task<PlaneDTO> GetPlaneById(int id)
		{
			var result = await GetPlanes();
			return result.Find(p => p.Id == id);
		}

		public async Task<List<PlaneDTO>> GetPlanes()
		{
			var departures = await unit.DeparturesRepo.GetEntities(includeProperties: "CrewItem,PlaneItem,TypeOfPlane", filter:( p => p.PlaneItem != null));
			List<Plane> planes = new List<Plane>();
			if (departures != null && departures.Count > 0)
			{
				foreach (var item in departures)
				{
					planes.Add(item.PlaneItem);
				}
				return mapper.Map<List<Plane>, List<PlaneDTO>>(planes) ?? throw new AutoMapperMappingException("Error: Can't map the plane into planeDTO"); ;
			}
			else
			{
				throw new Exception("Error: There isn't any plane.");
			}
		}

		public async Task<PlaneDTO> UpdatePlane(PlaneDTO value)
		{
			if (value != null)
			{
				Plane newPlane = mapper.Map<PlaneDTO, Plane>(value) ?? throw new AutoMapperMappingException("Error: Can't map the planeDTO into plane"); 
				var departures = await unit.DeparturesRepo.GetEntities(filter:(p => p.PlaneItem.Id.Equals(value.Id)));
				var departure = departures.Find(p => p.PlaneItem.Id.Equals(value.Id));
				departure.PlaneItem = newPlane;
				var result = await unit.DeparturesRepo.Update(departure);
				await unit.SaveChangesAsync();
				return mapper.Map<Plane,PlaneDTO>(result.PlaneItem) ?? throw new AutoMapperMappingException("Error: Can't map the plane into planeDTO"); 
			}
			else
			{
				throw new ArgumentNullException();
			}
		}

		public async Task<int> DeletePlane(int id)
		{
			var departures = await unit.DeparturesRepo.GetEntities(filter:(p => p.PlaneItem.Id == id));
			var departure = departures.Find(p => p.PlaneItem.Id.Equals(id));
			
			if (departure.PlaneItem != null)
			{
				departure.PlaneItem = null;
				await unit.DeparturesRepo.Update(departure);
				return await unit.SaveChangesAsync();
			}
			else
			{
				throw new Exception("Error: Can't find such plane to delete!");
			}
		}

	}
}
