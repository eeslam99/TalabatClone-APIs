namespace Route.Talabat.APIs.Helpers
{
	public class Pagination<T>
	{
		public int PageIndexs { get; set; }

        public int PageSize { get; set; }
		public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }

		public Pagination(int pageIndexs, int pageSize,int count, IReadOnlyList<T> data)
		{
			PageIndexs = pageIndexs;
			PageSize = pageSize;
			Data = data;
			Count = count;
		}
	}
}
