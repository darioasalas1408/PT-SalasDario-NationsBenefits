using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PT_SalasDario.API.Infra;
using PT_SalasDario.API.Requests;
using PT_SalasDario.Services;
using PT_SalasDario.Services.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PT_SalasDario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductListResponseDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Get([FromQuery] GetProductListRequest request)
        {
            try
            {
                var productList = await _productService.GetProducts(_mapper.Map<Services.Requests.GetProductListRequest>(request));
                return Ok(new ApiDataSetResult<List<ProductListResponseDTO>>(productList.Item1, productList.Item2, productList.Item3, productList.Item4, productList.Item5));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResult(StatusCodes.Status500InternalServerError) { Errors = [ex.Message.ToString()] });
            }
        }
    }
}
