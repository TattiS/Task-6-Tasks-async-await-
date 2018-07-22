using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.Repositories;
using DALProject.UnitOfWork;
using DTOLibrary.ApiDTOs;
using Newtonsoft.Json;

namespace AirportService.Services
{
   public class QueryService
    {
		private readonly string baseAddress = "http://5b128555d50a5c0014ef1204.mockapi.io/";
		private readonly IAsyncUOW unit;
		private  IMapper mapper;
		public QueryService(AsyncUnitOfWork unitOfWork)
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

				c.CreateMap<Crew, ApiCrew>().ReverseMap();

			});
			mapConfig.AssertConfigurationIsValid();
			if (mapper == null)
			{
				mapper = mapConfig.CreateMapper();
			}

		}

		public async Task <IEnumerable<Crew>> LoadCrews()
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.BaseAddress = baseAddress;
				string crewsStr = webClient.DownloadString("crew");
				List<ApiCrew> crews = JsonConvert.DeserializeObject<List<ApiCrew>>(crewsStr).FindAll(c=>c.Id<10).ToList();
				
				try
				{
					await WriteFile(crews);
					return mapper.Map<List<ApiCrew>,List<Crew>>(crews);
				}
				catch (Exception ex)
				{

					throw ex;
				}
			}
		}

		public async Task WriteFile(IEnumerable<ApiCrew> crews)
		{
			
			string date = DateTime.Now.ToShortDateString();
			string time = DateTime.Now.ToShortTimeString();
			string path = $"Log_{date}_{time}.csv";
			using (var textWriter = File.CreateText(path))
			using (var csv = new CsvWriter(textWriter))
			{

				csv.WriteRecords(crews);
				await csv.NextRecordAsync();

				
			}
		}

		public async Task WriteToDb(IEnumerable<ApiCrew> crews)
		{
			var currentCrews = mapper.Map<IEnumerable<ApiCrew>, IEnumerable<Crew>>(crews);
			 unit.CrewRepo.GetAll().AddRange(currentCrews);
			await unit.SaveChangesAsync();
		}

	}
    

}
