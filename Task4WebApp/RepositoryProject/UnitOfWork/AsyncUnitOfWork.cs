using System;
using System.Threading.Tasks;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.Repositories;

namespace DALProject.UnitOfWork
{
	public class AsyncUnitOfWork:IAsyncUOW, IDisposable
    {
		private MainDBContext mainDB;
		private IAsyncRepository<Flight> flights;
		private IAsyncRepository<Departure> departures;
		private IAsyncRepository<Stewardess> stewardesses;
		private IAsyncRepository<Pilot> pilots;
		private IAsyncRepository<PlaneType> planeTypes;
		private AsyncCrewRepository crews;

		public AsyncUnitOfWork(MainDBContext dBContext)
		{
			mainDB = dBContext;
		}

		public IAsyncRepository<Flight> FlightsRepo
		{
			get
			{
				if (this.flights == null)
				{
					this.flights = new AsyncRepository<Flight>(mainDB);
				}
				return this.flights;
			}
		}
		public IAsyncRepository<Departure> DeparturesRepo
		{
			get
			{
				if (this.departures == null)
				{
					this.departures = new AsyncDepartureRepository(mainDB);
				}
				return this.departures;
			}
		}
		public IAsyncRepository<Stewardess> StewardessesRepo
		{
			get
			{
				if (this.stewardesses == null)
				{
					this.stewardesses = new AsyncRepository<Stewardess>(mainDB);
				}
				return this.stewardesses;
			}
		}
		public IAsyncRepository<Pilot> PilotsRepo
		{
			get
			{
				if (this.pilots == null)
				{
					this.pilots = new AsyncRepository<Pilot>(mainDB);
				}
				return this.pilots;
			}
		}
		public IAsyncRepository<PlaneType> PlaneTypesRepo
		{
			get
			{
				if (this.planeTypes == null)
				{
					this.planeTypes = new AsyncRepository<PlaneType>(mainDB);
				}
				return this.planeTypes;
			}
		}
		public AsyncCrewRepository CrewRepo
		{
			get
			{
				if (this.crews == null)
				{
					this.crews = new AsyncCrewRepository(mainDB);
				}
				return this.crews;
			}
		}

		public async Task<int> SaveChangesAsync()
		{
			try
			{
				return await mainDB.SaveChangesAsync();
			}
			catch (Exception ex)
			{

				throw ex;
			}
		
		}



		#region IDisposable Support
		private bool disposedValue = false;
		public virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					mainDB.Dispose();
				}

				disposedValue = true;
			}
		}

		void IDisposable.Dispose()
		{
			Dispose(true);

		}
		#endregion
	}
}
