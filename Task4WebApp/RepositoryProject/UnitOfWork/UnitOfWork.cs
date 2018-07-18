using System;
using System.Linq;
using System.Linq.Expressions;
using DALProject.Interefaces;
using DALProject.Models;
using DALProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DALProject.UnitOfWork
{
	public class UnitOfWork:IUnitOfWork, IDisposable
    {
		private MainDBContext mainDB;
		private IRepository<Flight> flights;
		private IRepository<Departure> departures;
		private IRepository<Stewardess> stewardesses;
		private IRepository<Pilot> pilots;
		private IRepository<PlaneType> planeTypes;
		private IRepository<Crew> crews;

		public UnitOfWork(MainDBContext dBContext)
		{
			mainDB = dBContext;
		}

		public IRepository<Flight> FlightsRepo
		{
			get {
				if (this.flights == null)
				{
					this.flights = new Repository<Flight>(mainDB);
				}
				return this.flights;
			}
		}
		public IRepository<Departure> DeparturesRepo
		{
			get
			{
				if (this.departures == null)
				{
					this.departures = new DepartureRepository(mainDB);
				}
				return this.departures;
			}
		}
		public IRepository<Stewardess> StewardessesRepo
		{
			get
			{
				if (this.stewardesses == null)
				{
					this.stewardesses = new Repository<Stewardess>(mainDB);
				}
				return this.stewardesses;
			}
		}
		public IRepository<Pilot> PilotsRepo
		{
			get
			{
				if (this.pilots == null)
				{
					this.pilots = new Repository<Pilot>(mainDB);
				}
				return this.pilots;
			}
		}
		public IRepository<PlaneType> PlaneTypesRepo
		{
			get
			{
				if (this.planeTypes == null)
				{
					this.planeTypes = new Repository<PlaneType>(mainDB);
				}
				return this.planeTypes;
			}
		}
		public IRepository<Crew> CrewRepository
		{
			get
			{
				if (this.crews == null)
				{
					this.crews = new CrewRepository(mainDB);
				}
				return this.crews;
			}
		}


		public void SaveChanges()
		{
			mainDB.SaveChanges();
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
