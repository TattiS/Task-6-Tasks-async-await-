using System;
using System.Collections.Generic;
using System.Text;
using DALProject.Interefaces;
using DALProject.Models;

namespace Task4WebAppTests.Fakes
{
    public class FakeUnitOfWork:IUnitOfWork
    {
		private readonly Dictionary<Type, object> _store = new Dictionary<Type, object>();

		public void SetRepository<T>(IRepository<T> repository) where T : BaseEntity
		{
			_store[typeof(T)] = repository;
		}

		public IRepository<Flight> FlightsRepo
		{
			get
			{
				object _result;
				return _store.TryGetValue(typeof(Flight), out _result) ? _result as IRepository<Flight> : new FakeRepository<Flight>();
			}
		}

		public IRepository<Departure> DeparturesRepo
		{
			get
			{
				object _result;
				return _store.TryGetValue(typeof(Departure), out _result) ? _result as IRepository<Departure> : new FakeRepository<Departure>();
			}
		}

		public IRepository<Stewardess> StewardessesRepo
		{
			get
			{
				object _result;
				return _store.TryGetValue(typeof(Stewardess), out _result) ? _result as IRepository<Stewardess> : new FakeRepository<Stewardess>();
			}
		}

		public IRepository<Pilot> PilotsRepo
		{
			get
			{
				object _result;
				return _store.TryGetValue(typeof(Pilot), out _result) ? _result as IRepository<Pilot> : new FakeRepository<Pilot>();
			}
		}

		public IRepository<PlaneType> PlaneTypesRepo
		{
			get
			{
				object _result;
				return _store.TryGetValue(typeof(PlaneType), out _result) ? _result as IRepository<PlaneType> : new FakeRepository<PlaneType>();
			}
		}

		public void SaveChanges()
		{
			
		}
	}

}
