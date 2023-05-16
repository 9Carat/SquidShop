using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquidShopApi.Models.DTO;
using SquidShopApi.Models;
using SquidShopApi.Repository.IRepository;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace SquidShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderListController : Controller
	{
		private readonly IRepository<OrderList> _context;
		private readonly IMapper _mapper;
		protected ApiResponse _response;
		public OrderListController(IRepository<OrderList> context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
			_response = new();
		}
		//GET
		[HttpGet]
		public async Task<ActionResult<ApiResponse>> GetOrderList()
		{
			IEnumerable<OrderList> orders = await _context.GetAllAsync();
			_response.Result = _mapper.Map<List<OrderListDTO>>(orders);
			return Ok(_response);
		}
		//GET WITH ID
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse>> GetOrderList(int id)
		{
			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var orders = await _context.GetByIdAsync(p => p.FK_OrderId == id);
				if (orders == null)
				{
					return NotFound();
				}
				_response.Result = _mapper.Map<OrderListDTO>(orders);
				return Ok(_response);
			}
			catch (Exception)
			{

				throw;
			}
		}
		//CREATE/POST
		[HttpPost]
		public async Task<ActionResult<ApiResponse>> AddOrderList([FromBody] OrderListDTO orderListDTO)
		{
			if (orderListDTO == null)
			{
				return BadRequest(orderListDTO);
			}
			OrderList orders = _mapper.Map<OrderList>(orderListDTO);
			await _context.CreateAsync(orders);
			_response.Result = _mapper.Map<OrderListDTO>(orders);
			return CreatedAtAction(nameof(GetOrderList), new { id = orders.OrderListId }, _response);
		}

		//PATCH
		[HttpPatch("{id:int}")] //fixa mot user sen också
		public async Task<IActionResult> UpdatePartialOrderList(int id, JsonPatchDocument<OrderListUpdateDTO> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var orders = await _context.GetByIdAsync(o => o.OrderListId == id);
			OrderListUpdateDTO orderListUpdate = _mapper.Map<OrderListUpdateDTO>(orders);
			if (orderListUpdate == null)
			{
				return BadRequest();
			}
			patchDTO.ApplyTo(orderListUpdate, ModelState);
			OrderList model = _mapper.Map<OrderList>(orderListUpdate);
			await _context.UpdatePartialAsync(model);
			return NoContent();

		}
	}
}
