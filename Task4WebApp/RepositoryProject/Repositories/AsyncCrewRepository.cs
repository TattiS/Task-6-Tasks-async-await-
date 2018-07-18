using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DALProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DALProject.Repositories
{
	public class AsyncCrewRepository : AsyncRepository<Crew>
    {

		

		public AsyncCrewRepository(MainDBContext dBContext) : base(dBContext)
		{

		}

		public override async Task<List<Crew>> GetAllEntities()
		{
			return await dataSet.Include("Stewardesses").ToListAsync();
		}

		public override async Task<Crew> GetEntityById(object id)
		{
			return await dataSet.Include(p => p.Stewardesses).FirstAsync(p => p.Id == (int)id);
		}

		public override async Task<List<Crew>> GetEntities(Expression<Func<Crew, bool>> filter = null, Func<IQueryable<Crew>, IOrderedQueryable<Crew>> orderBy = null, string includeProperties = "")
		{
			IQueryable<Crew> query = base.context.Set<Crew>().Include(p => p.Stewardesses);

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
