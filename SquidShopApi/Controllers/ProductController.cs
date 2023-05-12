using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquidShopApi.Models;
using SquidShopApi.Models.DTO;
using SquidShopApi.Repository.IRepository;

namespace SquidShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
		private readonly IRepository<Product> _context;
		private readonly IMapper _mapper;
		protected ApiResponse _response;
        public ProductController(IRepository<Product> context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
			_response = new();
        }
		//GET
		[HttpGet]
		public async Task<ActionResult <ApiResponse>> GetProducts()
		{
			IEnumerable<Product> products = await _context.GetAllAsync();
			_response.Result = _mapper.Map<List<ProductDTO>>(products);
			return Ok(_response);
		}
		//GET WITH ID



		//CREATE/POST
		[HttpPost]
		public async Task<ActionResult<ApiResponse>> AddProduct([FromBody] ProductDTO productDTO)
		{
			if (await _context.GetByIdAsync(p => p.ProductName.ToLower() == productDTO.ProductName.ToLower()) != null)
			{
				return BadRequest();
			}
			if (productDTO == null) 
			{
				return BadRequest(productDTO);
			}
			Product product = _mapper.Map<Product>(productDTO);
			await _context.CreateAsync(product);
			_response.Result = _mapper.Map<ProductDTO>(product);
			return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId}, _response);
		}
    }
}
