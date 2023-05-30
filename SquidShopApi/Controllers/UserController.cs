using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquidShopApi.Models.DTO;
using SquidShopApi.Models;
using SquidShopApi.Repository.IRepository;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace SquidShopApi.Controllers
{
    // Under construction***********

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IRepository<User> _context;
        //private readonly IRepository<IdentityUser> _iu;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public UserController(IRepository<User> context, IMapper mapper, IRepository<IdentityUser> iu)
        {
            _context = context;
            _mapper = mapper;
            //_iu = iu;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetUser()
        {
            try
            {
                IEnumerable<User> userList = await _context.GetAllAsync();
                _response.Result = _mapper.Map<List<UserDTO>>(userList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var user = await _context.GetByIdAsync(u => u.UserId == id);
                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<UserDTO>(user);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] UserCreateDTO createDto)
        {
            try
            {
                //if (await _context.GetByIdAsync(u => u.Id == createDto.FK_UsersId) != null)
                //{
                //    ModelState.AddModelError("Custom error", "This user already exist");
                //    return BadRequest(ModelState);
                //}
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                User user = _mapper.Map<User>(createDto);
                // user.FK_userid = SignedInUser id ??? 
                await _context.CreateAsync(user);
                _response.Result = _mapper.Map<UserDTO>(user);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetUser", new { id = user.UserId}, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var user = await _context.GetByIdAsync(u => u.UserId == id);
                //var netuser = await _iu.GetByIdAsync(u => u.Id == user.FK_UsersId);
                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _context.RemoveAsync(user);
                //await _iu.RemoveAsync(netuser);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateCategory(int id, [FromBody] UserUpdateDTO updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.UserId)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                User model = _mapper.Map<User>(updateDto);

                await _context.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
