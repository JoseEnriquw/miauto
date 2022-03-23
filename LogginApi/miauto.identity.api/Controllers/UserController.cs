using miauto.identity.api.Models.Entity;
using miauto.identity.api.Models.Request;
using miauto.identity.api.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace miauto.identity.api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositories _userRepositories;
        public UserController(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userRepositories.GetAllUser());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userRepositories.GetUser(id));
        }
        [HttpPost("..login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest authRequest)
        {
            if (authRequest == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           

            return Ok(await _userRepositories.Auth(authRequest));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userRepositories.InsertDefaultUser(user);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            await _userRepositories.UpdateUser(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _userRepositories.DeleteUser(id);

            return NoContent();
        }

    }
}
