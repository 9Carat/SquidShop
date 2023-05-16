using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquidShopApi.Models;
using SquidShopApi.Models.DTO;
using SquidShopApi.Repository.IRepository;
using System.Net;

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
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult <ApiResponse>> GetProduct()
		{
			try
			{
				IEnumerable<Product> products = await _context.GetAllAsync();
				_response.Result = _mapper.Map<List<ProductDTO>>(products);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.ToString() };
			}
			return _response;
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
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}
				var product = await _context.GetByIdAsync(p => p.ProductId == id);
				if (product == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound();
				}
				_response.Result = _mapper.Map<ProductDTO>(product);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		//CREATE/POST
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> AddProduct([FromBody] ProductDTO productDTO)
		{
			try
			{
				if (await _context.GetByIdAsync(p => p.ProductName.ToLower() == productDTO.ProductName.ToLower()) != null)
				{
					return BadRequest(ModelState);
				}
				if (productDTO == null)
				{
					return BadRequest(productDTO);
				}
				Product product = _mapper.Map<Product>(productDTO);
				await _context.CreateAsync(product);
				_response.Result = _mapper.Map<ProductDTO>(product);
				_response.StatusCode = HttpStatusCode.OK;	
				return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.ToString() };
			}
			return _response;
		}

		//UPDATE
		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
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
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		//DELETE
		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
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
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;	
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}
    }
}
