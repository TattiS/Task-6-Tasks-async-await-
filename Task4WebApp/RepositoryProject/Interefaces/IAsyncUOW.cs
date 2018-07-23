using System.Threading.Tasks;
using DALProject.Models;
using DALProject.Repositories;

namespace DALProject.Interefaces
{
	public interface IAsyncUOW
    {
		IAsyncRepository<Flight> FlightsRepo { get; }
		IAsyncRepository<Departure> DeparturesRepo { get; }
		IAsyncRepository<Stewardess> StewardessesRepo { get; }
		IAsyncRepository<Pilot> PilotsRepo { get; }
		IAsyncRepository<PlaneType> PlaneTypesRepo { get; }
		AsyncCrewRepository CrewRepo { get; }
		Task<int> SaveChangesAsync();
	}
}
