using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DALProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DALProject.Repositories
{
	public class CrewRepository : Repository<Crew> 
	{

		public CrewRepository(MainDBContext dBContext): base(dBContext)
		{
			
		}
		public override List<Crew> GetEntities(Expression<Func<Crew, bool>> filter = null, Func<IQueryable<Crew>, IOrderedQueryable<Crew>> orderBy = null, string includeProperties = "")
		{
			IQueryable<Crew> query = base.context.Set<Crew>().Include(p=>p.Stewardesses);
			
			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query?.ToList();
			}
		}

		
	}
}
