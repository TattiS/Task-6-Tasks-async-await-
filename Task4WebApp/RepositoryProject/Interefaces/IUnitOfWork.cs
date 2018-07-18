using System;
using System.Collections.Generic;
using System.Text;
using DALProject.Models;
using DALProject.Repositories;

namespace DALProject.Interefaces
{
   public interface IUnitOfWork
    {
		IRepository<Flight> FlightsRepo { get;  }
		IRepository<Departure> DeparturesRepo { get;  }
		IRepository<Stewardess> StewardessesRepo { get; }
		IRepository<Pilot> PilotsRepo { get;  }
		IRepository<PlaneType> PlaneTypesRepo { get;  }

		void SaveChanges();
	}
}
