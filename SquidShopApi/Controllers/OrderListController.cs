using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquidShopApi.Models.DTO;
using SquidShopApi.Models;
using SquidShopApi.Repository.IRepository;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;

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
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ApiResponse>> GetOrderList()
		{
			try
			{
				IEnumerable<OrderList> orders = await _context.GetAllAsync();
				_response.Result = _mapper.Map<List<OrderListDTO>>(orders);
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
		public async Task<ActionResult<ApiResponse>> GetOrderList(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest();
				}
				var orders = await _context.GetByIdAsync(p => p.FK_OrderId == id);
				if (orders == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound();
				}
				_response.Result = _mapper.Map<OrderListDTO>(orders);
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
		public async Task<ActionResult<ApiResponse>> AddOrderList([FromBody] OrderListDTO orderListDTO)
		{
			try
			{
				if (orderListDTO == null)
				{
					return BadRequest(orderListDTO);
				}
				OrderList orders = _mapper.Map<OrderList>(orderListDTO);
				await _context.CreateAsync(orders);
				_response.Result = _mapper.Map<OrderListDTO>(orders);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtAction(nameof(GetOrderList), new { id = orders.OrderListId }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.ToString() };
			}
			return _response;
		}

		//PATCH
		[HttpPatch("{id:int}")] //fixa mot user sen också?
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
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
