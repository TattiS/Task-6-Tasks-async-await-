using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DALProject.Models;
using Newtonsoft.Json;

namespace AirportService.Services
{
   public class QueryService
    {
		private readonly string baseAddress = "http://5b128555d50a5c0014ef1204.mockapi.io/";

		public async Task <IEnumerable<Crew>> GetCrews()
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.BaseAddress = baseAddress;
				string crewsStr = webClient.DownloadString("crew");
				List<Crew> crews = JsonConvert.DeserializeObject<List<Crew>>(crewsStr);
				
				try
				{
					await WriteFile(crews);
					return crews;
				}
				catch (Exception ex)
				{

					throw ex;
				}
			}
		}

		public async Task WriteFile(IEnumerable<Crew> crews)
		{
			string date = DateTime.Now.ToShortDateString();
			string time = DateTime.Now.ToShortTimeString();
			string path = @"Log_{date}_{time}.csv";
			using (var textWriter = File.CreateText(path))
			using (var csv = new CsvWriter(textWriter))
			{

				csv.WriteRecords(crews);
				await csv.NextRecordAsync();

				
			}
		}

	}
    

}
