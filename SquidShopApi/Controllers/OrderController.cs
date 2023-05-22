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
	public class OrderController : Controller
	{
		private readonly IRepository<Order> _context;
		private readonly IMapper _mapper;
		protected ApiResponse _response;
		public OrderController(IRepository<Order> context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
			_response = new();
		}
		//GET
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ApiResponse>> GetOrder()
		{
			try
			{
				IEnumerable<Order> products = await _context.GetAllAsync();
				_response.Result = _mapper.Map<List<OrderDTO>>(products);
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
		public async Task<ActionResult<ApiResponse>> GetOrder(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}
				var product = await _context.GetByIdAsync(p => p.OrderId == id);
				if (product == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				_response.Result = _mapper.Map<OrderDTO>(product);
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
		public async Task<ActionResult<ApiResponse>> AddOrder([FromBody] OrderDTO orderDTO)
		{
			try
			{
				if (await _context.GetByIdAsync(o => o.OrderId == orderDTO.OrderId) != null)
				{
					return BadRequest(ModelState);
				}
				if (orderDTO == null)
				{
					return BadRequest(orderDTO);
				}
				Order order = _mapper.Map<Order>(orderDTO);
				await _context.CreateAsync(order);
				_response.Result = _mapper.Map<OrderDTO>(order);
				_response.StatusCode = HttpStatusCode.OK;
				return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.ToString() };
			}
			return _response;
		}
	}
}
