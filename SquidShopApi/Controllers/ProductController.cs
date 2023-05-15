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
		public async Task<ActionResult <ApiResponse>> GetProduct()
		{
			IEnumerable<Product> products = await _context.GetAllAsync();
			_response.Result = _mapper.Map<List<ProductDTO>>(products);
			return Ok(_response);
		}
		//GET WITH ID
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse>> GetProduct(int id)
		{
			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var product = await _context.GetByIdAsync(p => p.ProductId == id);
				if (product == null)
				{
					return NotFound();
				}
				_response.Result = _mapper.Map<ProductDTO>(product);
				return Ok(_response);
			}
			catch (Exception)
			{

				throw;
			}
		}

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
			return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId}, _response);
		}

		//UPDATE
		[HttpPut("{id:int}")]
		public async Task<ActionResult<ApiResponse>> UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDTO)
		{
			try
			{
				if (updateDTO == null || id != updateDTO.ProductId)
				{
					return BadRequest();
				}
				Product product = _mapper.Map<Product>(updateDTO);
				await _context.UpdateAsync(product);
				return Ok(_response);
			}
			catch (Exception)
			{

				throw;
			}
		}

		//DELETE
		[HttpDelete("{id:int}")]
		public async Task<ActionResult<ApiResponse>> DeleteProduct(int id)
		{
			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var product = await _context.GetByIdAsync(p => p.ProductId == id);
				if (product == null)
				{
					return NotFound();
				}
				await _context.RemoveAsync(product);
				return Ok(_response);
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
