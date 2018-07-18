using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DALProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DALProject.Repositories
{
    public class DepartureRepository:Repository<Departure>
    {
		public DepartureRepository(MainDBContext dBContext) : base(dBContext)
		{

		}
		public override List<Departure> GetEntities(Expression<Func<Departure, bool>> filter = null, Func<IQueryable<Departure>, IOrderedQueryable<Departure>> orderBy = null, string includeProperties = "")
		{
			IQueryable<Departure> query = base.context.Set<Departure>().Include(a => a.CrewItem.Stewardesses).Include(a => a.CrewItem).Include(a => a.PlaneItem);

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
