
using authenticat.Models;
using authenticat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwtTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItem _item;
        public ItemController(IItem item)
        {
            _item = item;
        }

        [Authorize(Roles ="admin")]
        [HttpPost("create-item")]
        public IActionResult CreateItem(Items item)
        {
            _item.CreateItem(item);
            return NoContent();
        }
        [Authorize]
        [HttpGet("getTask")]
        public IActionResult GetItems()
        {
            return Ok(_item.GetItems());
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("delete")]

        public IActionResult DeleteItem(int id)
        {
            _item.DeleteItem(id);
            return NoContent();
        }
    }
}
