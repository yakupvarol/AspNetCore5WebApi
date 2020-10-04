using Business.Abstract;
using DTO.WebApi;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Helper.ReturnMessage;

namespace WebApi.Controllers
{
    //[Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

       
        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ReturnData<IEnumerable<UserAll>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var dt = await _userService.GetAllAsync();
            return Ok(new ReturnData { Data = dt, Count = dt.Count() });
        }

        [HttpGet("{id}")]
        [MapToApiVersion("2")]
        [ProducesResponseType(typeof(ReturnData<UserGet>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            var dt = await _userService.GetByIdAsync(id);
            if (dt == null)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReturnData<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] Users frm)
        {
            var dt = await _userService.CreateAsync(frm);

            return Ok(new ReturnData { Data = dt, Code = 201 });
        }

        [HttpPut]
        [ProducesResponseType(typeof(ReturnData<bool>), (int)HttpStatusCode.Created)]
        public IActionResult Update([FromBody] User frm)
        {
            var dt = _userService.Edit(frm);
            if (dt == 0)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = Convert.ToBoolean(dt) });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ReturnData<bool>), (int)HttpStatusCode.OK)]
        public IActionResult DeleteById(int id)
        {
            var dt = _userService.RemoveById(id);
            if (dt == false)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }



        [HttpGet("Get")]
        [ProducesResponseType(typeof(ReturnData<UserGet>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] UserGetRequest src)
        {
            var dt = await _userService.GetAsync(src);
            if (dt == null)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }

        [HttpGet("GetPagedList")]
        [ProducesResponseType(typeof(ReturnList<GetPagedList>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] GetPagedListRequest src)
        {
            var dt = await _userService.GetPagedListAsync(src);
            if (dt == null)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnList<IList<UserAll>> { Data = new DataList<IList<UserAll>> { itemCount = dt.itemCount, items = dt.items, page = src.page, rowCount = src.rowCount, sort = src.sort } });
        }



        [HttpPost("Insert")]
        [ProducesResponseType(typeof(ReturnData<Users>), (int)HttpStatusCode.OK)]
        public IActionResult Insert([FromBody] User frm)
        {
            var dt = _userService.Create(frm);

            return Ok(new ReturnData { Data = dt }); ;
        }



        [HttpPut("UpdateOne")]
        [ProducesResponseType(typeof(ReturnData<bool>), (int)HttpStatusCode.OK)]
        public IActionResult UpdateOne([FromBody] User frm)
        {
            var dt = _userService.Update(frm);
            if (dt == false)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }

        [HttpPut("UpdateTwo")]
        [ProducesResponseType(typeof(ReturnData<Users>), (int)HttpStatusCode.OK)]
        public IActionResult UpdateTwo([FromBody] Users frm)
        {
            var dt = _userService.Update(frm);
            if (dt == null)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }

        [HttpPut("UpdateThree")]
        [ProducesResponseType(typeof(ReturnData<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateThreeAsync([FromBody] User frm)
        {
            var dt = await _userService.UpdateAsync(frm);
            if (dt == false)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }



        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(ReturnData<bool>), (int)HttpStatusCode.OK)]
        public IActionResult DeleteBy([FromBody] Users frm)
        {
            var dt = _userService.DeleteBy(frm);
            if (dt == false)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }


        [HttpDelete("FindRemove")]
        [ProducesResponseType(typeof(ReturnData<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindRemoveByAsync([FromBody] UserGetRequest frm)
        {
            var dt = await _userService.FindRemoveByAsync(frm);
            if (dt == false)
                return Ok(new ReturnNotFound());

            return Ok(new ReturnData { Data = dt });
        }

    }
}
