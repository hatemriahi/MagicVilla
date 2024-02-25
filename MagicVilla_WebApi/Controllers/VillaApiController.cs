using MagicVilla_WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_WebApi.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas()
        {
            return new List<Villa>
            {
                new Villa{ Id = Guid.NewGuid(), Name = "Villa" },
                new Villa{ Id = Guid.NewGuid(), Name = "Villa" },
            };
        }
    }
}
