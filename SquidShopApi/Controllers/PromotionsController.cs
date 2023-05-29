using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquidShopApi.Data;
using SquidShopApi.Models;
using SquidShopApi.Models.DTO;
using SquidShopApi.Repository;
using SquidShopApi.Repository.IRepository;


namespace SquidShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : Controller
    {
        private readonly IPromotionRepository _context;
        private readonly IMapper _mapper;
        protected ApiResponse _response;

        public PromotionsController(IPromotionRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new();
        }

        // GET: api/Promotions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetPromotions()
        {
            try
            {
                IEnumerable<Promotion> promotions = await _context.GetAllAsync();
                _response.Result = _mapper.Map<List<PromotionDTO>>(promotions);
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

        // GET: api/Promotions/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetPromotion(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var promotion = await _context.GetByIdAsync(p => p.PromotionId == id);
                if (promotion == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }
                _response.Result = _mapper.Map<PromotionDTO>(promotion);
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

        // PUT: api/Promotions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateProduct(int id, [FromBody] PromotionDTO promotionDTO)
        {
            try
            {
                if (promotionDTO == null || id != promotionDTO.PromotionId)
                {
                    return BadRequest();
                }
                Promotion promotion = _mapper.Map<Promotion>(promotionDTO);
                await _context.UpdateAsync(promotion);
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

        // POST: api/Promotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddPromotion(PromotionDTO promotionDTO)
        {
            try
            {
                if (await _context.GetByIdAsync(p => p.PromotionId == promotionDTO.PromotionId) != null)
                {
                    return BadRequest(ModelState);
                }
                if (promotionDTO == null)
                {
                    return BadRequest(promotionDTO);
                }
                Promotion promotion = _mapper.Map<Promotion>(promotionDTO);
                await _context.CreateAsync(promotion);
                _response.Result = _mapper.Map<PromotionDTO>(promotion);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtAction(nameof(GetPromotion), new { id = promotion.PromotionId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        // DELETE: api/Promotions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePromotion(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var promotion = await _context.GetByIdAsync(p => p.PromotionId == id);
                if (promotion == null)
                {
                    return NotFound();
                }
                await _context.RemoveAsync(promotion);
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