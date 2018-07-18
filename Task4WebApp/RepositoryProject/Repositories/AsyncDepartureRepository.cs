using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DALProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DALProject.Repositories
{
	public class AsyncDepartureRepository : AsyncRepository<Departure>
    {
		public AsyncDepartureRepository(MainDBContext dBContext) : base(dBContext)
		{

		}

		public override async Task<List<Departure>> GetAllEntities()
		{
			return await dataSet.Include(p=>p.CrewItem).Include(p=>p.CrewItem.Stewardesses).Include(p=>p.PlaneItem).Include(p=>p.PlaneItem.TypeOfPlane).ToListAsync();
		}

		public override async Task<Departure> GetEntityById(object id)
		{
			return await dataSet.Include(p => p.CrewItem).Include(p => p.CrewItem.Stewardesses).Include(p => p.PlaneItem).Include(p => p.PlaneItem.TypeOfPlane).FirstAsync(p => p.Id == (int)id);
		}

		public override async Task<List<Departure>> GetEntities(Expression<Func<Departure, bool>> filter = null, Func<IQueryable<Departure>, IOrderedQueryable<Departure>> orderBy = null, string includeProperties = "")
		{
			IQueryable<Departure> query = base.context.Set<Departure>().Include(a => a.CrewItem.Stewardesses).Include(a => a.CrewItem).Include(a => a.PlaneItem).Include(p => p.PlaneItem.TypeOfPlane);

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				return await orderBy(query).ToListAsync();
			}
			else
			{
				return await query?.ToListAsync();
			}
		}


	}
}
