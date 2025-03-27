using System.ComponentModel.DataAnnotations;

namespace PT_SalasDario.API.Requests
{
    public class GetPaginatedListBaseRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than or equal to 1.")]
        public int? Page { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than or equal to 1.")]
        public int? PageSize { get; set; } = 3;
    }
}
