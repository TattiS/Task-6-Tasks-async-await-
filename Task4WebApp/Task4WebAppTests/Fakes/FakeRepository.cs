using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DALProject.Interefaces;
using DALProject.Models;

namespace Task4WebAppTests.Fakes
{
    public class FakeRepository<TEntity>:IRepository<TEntity> where TEntity : BaseEntity
    {

		public readonly List<TEntity> dataSet;
		public FakeRepository(params TEntity[] data)
		{

			this.dataSet = new List<TEntity>(data);
		}
		public IQueryable<TEntity> Query()
		{
			return dataSet.AsQueryable();
		}
		public List<TEntity> GetEntities(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
		{
			IQueryable<TEntity> query = Query();

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

		public TEntity GetEntityById(object id)
		{
			return dataSet.FirstOrDefault(p => p.Id == (int)id);
		}

		public void Insert(TEntity entity)
		{
			dataSet.Add(entity);
		}

		public void Update(TEntity entityToUpdate)
		{
			
		}

		public void Delete(int id)
		{
			var del = dataSet.First(p=>p.Id==id);
			Delete(del);
		}

		public void Delete(TEntity entityToDelete)
		{
			if (entityToDelete != null && dataSet.Contains(entityToDelete))
				{
				dataSet.Remove(entityToDelete);
			}
		}
	}
}
