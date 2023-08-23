using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BMWFanClub.InAndOutModel;
using BMWFanClub.Interfaces;


namespace ServisTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MUser.Form form)
        {
            try
            {
                var o = _user.Add(form, null).Result;

                return await Task.FromResult(StatusCode(o.Status, o));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var o = _user.Delete(id, null).Result;
            return await Task.FromResult(StatusCode(o.Status, o));

        }

        [HttpPost]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromForm] MUser.FilterForm form)
        {
            var o = _user.MultipleGet(form, null).Result;
            return await Task.FromResult(StatusCode(o.Status, o));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SingleGet(string id)
        {
            var o = _user.SingleGet(id, null).Result;
            return await Task.FromResult(StatusCode(o.Status, o));

        }
    }
}
