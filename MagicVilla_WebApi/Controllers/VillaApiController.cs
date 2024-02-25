using MagicVilla_WebApi.Models;
using MagicVilla_WebApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_WebApi.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            return new List<VillaDto>
            {
                new VillaDto{ Id = Guid.NewGuid(), Name = "Villa Tunis" },
                new VillaDto{ Id = Guid.NewGuid(), Name = "Villa Marsa" },
            };
        }
    }
}
