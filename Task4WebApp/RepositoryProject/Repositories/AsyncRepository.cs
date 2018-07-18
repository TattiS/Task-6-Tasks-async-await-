using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DALProject.Interefaces;
using DALProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DALProject.Repositories
{
	public class AsyncRepository<TEntity>:IAsyncRepository<TEntity> where TEntity:BaseEntity
    {
		protected readonly MainDBContext context;
		protected DbSet<TEntity> dataSet;
		public AsyncRepository(MainDBContext dBContext)
		{
			this.context = dBContext ?? throw new ArgumentNullException(nameof(dBContext),@"Data context can't be null.");
			this.dataSet = this.context.Set<TEntity>();
		}

		public virtual async Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
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
				return await orderBy(query).ToListAsync();
			}
			else
			{
				return await query?.ToListAsync();
			}
		}

		public virtual async Task<TEntity> GetEntityById(object id)
		{
			return await dataSet.FindAsync(id);
		}

		public virtual async Task<List<TEntity>> GetAllEntities()
		{
			return await dataSet.ToListAsync();
		}

		public virtual async Task<TEntity> Insert(TEntity entity)
		{
			 await dataSet.AddAsync(entity);
			return entity;
		}

		public virtual async Task<TEntity> Update(TEntity entityToUpdate)
		{
			if (entityToUpdate == null)
			{
				return null;
			}
				
			TEntity exist = await dataSet.FindAsync(entityToUpdate.Id);
			if (exist != null)
			{
				context.Entry(exist).CurrentValues.SetValues(entityToUpdate);
				await context.SaveChangesAsync();
			}
			return exist;
		}

		public virtual async Task<int> Delete(int id)
		{
			TEntity entityToDelete = await dataSet.FindAsync(id);
			return await Delete(entityToDelete);
		}

		public virtual async Task<int> Delete(TEntity entityToDelete)
		{
			if (this.context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dataSet.Attach(entityToDelete);
			}
			dataSet.Remove(entityToDelete);
			return await context.SaveChangesAsync();
		}
	}
}
