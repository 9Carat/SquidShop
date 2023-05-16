using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquidShopApi.Models;
using SquidShopApi.Models.DTO;
using SquidShopApi.Repository.IRepository;

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
		public async Task<ActionResult<ApiResponse>> GetOrder()
		{
			IEnumerable<Order> products = await _context.GetAllAsync();
			_response.Result = _mapper.Map<List<OrderDTO>>(products);
			return Ok(_response);
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
					return BadRequest();
				}
				var product = await _context.GetByIdAsync(p => p.OrderId == id);
				if (product == null)
				{
					return NotFound();
				}
				_response.Result = _mapper.Map<OrderDTO>(product);
				return Ok(_response);
			}
			catch (Exception)
			{

				throw;
			}
		}
		//CREATE/POST
		[HttpPost]
		public async Task<ActionResult<ApiResponse>> AddOrder([FromBody] OrderDTO orderDTO)
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
			return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, _response);
		}
	}
}
