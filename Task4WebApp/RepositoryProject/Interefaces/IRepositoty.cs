using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DALProject.Interefaces
{
	public interface IRepository<TEntity> where TEntity:class
    {
		List<TEntity> GetEntities(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
		TEntity GetEntityById(object id);
		void Insert(TEntity entity);
		void Update(TEntity entityToUpdate);
		void Delete(int id);
		void Delete(TEntity entityToDelete);
	}
}
