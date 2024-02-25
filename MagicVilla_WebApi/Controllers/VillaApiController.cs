using MagicVilla_WebApi.Data;
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
            return VillaStore.VillaList;
        }
        [HttpGet("VillaDetails/")]
        public IActionResult GetVilla([FromQuery] Guid id)
        {
            var villaToReturn = VillaStore.VillaList.SingleOrDefault(villa => villa.Id == id);
            if (villaToReturn == null)
            {
                return NotFound();
            }
            return Ok(villaToReturn);
        }
    }
}
