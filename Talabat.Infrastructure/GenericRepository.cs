using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Infrastructure.Data;

namespace Talabat.Infrastructure
{
	public class GenericRepository<T> : IGenaricRepository<T> where T : BaseEntity
	{
		private readonly StoreContext dbContext;

		public GenericRepository(StoreContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Add(T entity)
		=> dbContext.Set<T>().Add(entity);

		public void Delete(T entity)
		=> dbContext.Set<T>().Remove(entity);

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			//if(typeof(T) == typeof(Product)) 
			//	return (IEnumerable<T>) await dbContext.Set<Product>().AsNoTracking().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			return await dbContext.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			//return await dbContext.Set<Product>().Where(p => p.Id == id).AsNoTracking().Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
			return await dbContext.Set<T>().FindAsync(id);
		}

		public async Task<int> GetCountAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}

		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}

		public void Update(T entity)
		=> dbContext.Set<T>().Update(entity);

		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
		{
			return SpecificationsEvaluator<T>.GetQuery(dbContext.Set<T>(), spec);
		}
	}
}
