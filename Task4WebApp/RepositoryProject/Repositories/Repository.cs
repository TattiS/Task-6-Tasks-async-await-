using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using DALProject.Interefaces;
using Microsoft.EntityFrameworkCore;

namespace DALProject.Repositories
{
	public class Repository<TEntity>:IRepository<TEntity> where TEntity:class
    {
		protected readonly MainDBContext context;
		protected DbSet<TEntity> dataSet;
		public Repository(MainDBContext dBContext)
		{
			this.context = dBContext;
			this.dataSet = this.context.Set<TEntity>();
		}
		
		public virtual TEntity GetEntityById(object id)
		{
			return this.dataSet.Find(id);
		}
		public virtual void Delete(int id)
		{
			TEntity entityToDelete = dataSet.Find(id);
			Delete(entityToDelete);
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (this.context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dataSet.Attach(entityToDelete);
			}
			dataSet.Remove(entityToDelete);
		}

		public virtual List<TEntity> GetEntities(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = this.dataSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query?.Include(includeProperty);
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

		public void Insert(TEntity entity)
		{
			dataSet.Add(entity);
		}

		public virtual void Update(TEntity entityToUpdate)
		{
			if (entityToUpdate == null)
			{
				throw new ArgumentNullException(nameof(entityToUpdate), "There is no entity for updating");
			}
			dataSet.Attach(entityToUpdate);
			context.Entry(entityToUpdate).State = EntityState.Modified;
		}

	}


}
