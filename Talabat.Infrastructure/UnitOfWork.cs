using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Infrastructure.Data;

namespace Talabat.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext dbContext;

		//private Dictionary<string, GenericRepository<BaseEntity>> _repositories;

		private Hashtable _repositories;



		public UnitOfWork(StoreContext dbContext)
        {
			this.dbContext = dbContext;
			_repositories = new Hashtable();
		}

		public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var key = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(key))
			{
				var repository = new GenericRepository<TEntity>(dbContext);
				_repositories.Add(key, repository);
			} 

			return _repositories[key] as IGenaricRepository<TEntity>;
				
		}

		public Task<int> CompleteAsync()
			=> dbContext.SaveChangesAsync();

		public ValueTask DisposeAsync()
			=> dbContext.DisposeAsync();


	}
}
