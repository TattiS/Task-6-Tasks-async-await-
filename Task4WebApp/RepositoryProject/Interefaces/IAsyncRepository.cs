using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DALProject.Interefaces
{
	public interface IAsyncRepository<TEntity> where TEntity : class
	{
		Task<List<TEntity>> GetEntities(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
		Task<TEntity> GetEntityById(object id);
		Task<List<TEntity>> GetAllEntities();
		Task<TEntity> Insert(TEntity entity);
		Task<TEntity> Update(TEntity entityToUpdate);
		Task<int> Delete(int id);
		Task<int> Delete(TEntity entityToDelete);
    }
}
