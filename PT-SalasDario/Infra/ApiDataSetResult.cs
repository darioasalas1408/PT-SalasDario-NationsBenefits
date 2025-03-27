using System.Collections;

namespace PT_SalasDario.API.Infra
{
    public class ApiDataSetResult<T>
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public T Data { get; set; }

        public ApiDataSetResult()
        {
        }

        //TODO: Change this by a model
        public ApiDataSetResult((T, int, int, int, int) tuple) : this(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5)
        {
        }

        public ApiDataSetResult(T data)
        {
            Data = data;
            TotalCount = (data as IList)?.Count ?? 0;
        }

        public ApiDataSetResult(T data, int totalCount, int totalPages,  int currentPage, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }
}
