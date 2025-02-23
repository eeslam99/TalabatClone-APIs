using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity
	{
        public Expression<Func<T,bool>> Criteria { get; set; } // p => p.id == 1   criteria will sent to where methode 

		public List<Expression<Func<T,object>>> Includes { get; set; } // p => p.Brand  expresions will sent as includes 


        public Expression<Func<T,object>> OrderBy { get; set; }
		public Expression<Func<T,object>> OrderByDesc { get; set; }

        public int Skip { get; set; }

		public int Take { get; set; }

		public bool IsPaginationEnabled { get; set; }


    }
}
