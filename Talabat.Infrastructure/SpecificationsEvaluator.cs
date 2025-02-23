using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Infrastructure
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
		{

			var query = inputQuery; // _dbContext.set<Product>()

			if (spec.Criteria is not null) // p => p.id == 1 
			{
				query = query.Where(spec.Criteria);
			}

			if (spec.OrderBy is not null) // p => p.name 
				query = query.OrderBy(spec.OrderBy);

			else if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);

			if (spec.IsPaginationEnabled)
				query = query.Take(spec.Take).Skip(spec.Skip);
			///query = _dbcontext.set<Product>().Where(p => p.id == 1);
			/// includes 
			/// 1. p => p.brand 
			/// 2 p => p.category 
			
			query = spec.Includes.Aggregate(query,(currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			/// _dbcontext.set<Product>().Where(p => p.id == 1).Include(p => p.brand)
			///_dbcontext.set<Product>().Where(p => p.id == 1).Include(p => p.brand).Include(p => p.category)

			return query;
		}
	}
}
