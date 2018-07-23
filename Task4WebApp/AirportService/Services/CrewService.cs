using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AirportService.Interfaces;
using AutoMapper;
using CsvHelper;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.UnitOfWork;
using DTOLibrary.ApiDTOs;
using DTOLibrary.DTOs;
using Newtonsoft.Json;

namespace AirportService.Services
{
	public class CrewService: ICrewService
    {
		private readonly string baseAddress = "http://5b128555d50a5c0014ef1204.mockapi.io/";
		private static IAsyncUOW unit;
		private static IMapper mapper;

		public CrewService(AsyncUnitOfWork unitOfWork)
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
				c.CreateMap<ApiStewardess, Stewardess>().ForMember(e => e.Name, p => p.MapFrom(l => l.FirstName))
																					.ForMember(e => e.Surname, p => p.MapFrom(l => l.LastName))
																					.ForMember(e=> e.Id, p=>p.UseValue(0)).ReverseMap();
				c.CreateMap<ApiCrew, Crew>().ForMember(e => e.Id, p => p.UseValue(0))
																.ForMember(e => e.PilotId, p=>p.MapFrom(l=>l.Pilot.FirstOrDefault().Id))
																.ForMember(e => e.Stewardesses, p => p.MapFrom(l=>l.Stewardess)).ReverseMap();

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
			if (crew == null)
			{
				throw new ArgumentOutOfRangeException(nameof(crew));
			}
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


		public List<ApiCrew> LoadCrews()
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.BaseAddress = baseAddress;

				string crewsStr = webClient.DownloadString("crew");
				JsonSerializerSettings settings = new JsonSerializerSettings();
				settings.Formatting = Formatting.Indented;
				settings.DateFormatString = "YYYY-MM-DDTHH:mm:ss.FFFZ";
				List<ApiCrew> crews = JsonConvert.DeserializeObject<List<ApiCrew>>(crewsStr, settings).FindAll(c => c.Id < 10).ToList();

				try
				{
					return crews;
				}
				catch (Exception ex)
				{

					throw ex;
				}
			}
		}

		public async Task WriteFile(IEnumerable<ApiCrew> crews)
		{

			string date = DateTime.Now.ToString("yyyyMMddHHmmss");
			string path = $"Log_{date}.csv";
			using (var textWriter = File.CreateText(path))
			using (var csv = new CsvWriter(textWriter))
			{
				foreach (var item in crews)
				{
					csv.WriteRecord(item.Id);
					csv.WriteRecords(item.Pilot);
					csv.WriteRecords(item.Stewardess);
				}
				
				await csv.NextRecordAsync();


			}
		}

		public async Task WriteToDb(IEnumerable<ApiCrew> crews)
		{
			var currentCrews = mapper.Map<IEnumerable<ApiCrew>, IEnumerable<Crew>>(crews);
			await unit.CrewRepo.AddRange(currentCrews.ToList());
			await unit.SaveChangesAsync();
		}
	}
	
}
