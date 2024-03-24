using MagicVilla_WebApi.Data;
using MagicVilla_WebApi.Models;
using MagicVilla_WebApi.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_WebApi.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : Controller
    {
        private const string VillaDetailsRouteName = "VillaDetails";

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpGet("AllVillas")]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return VillaStore.VillaList;
        }

        //[HttpGet("VillaDetails/{id:guid}")]
        //public IActionResult GetVilla(Guid id)

        [HttpGet($"{VillaDetailsRouteName}/", Name = VillaDetailsRouteName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVilla([FromQuery] Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var villaToReturn = VillaStore.VillaList.SingleOrDefault(villa => villa.Id == id);
            if (villaToReturn == null)
            {
                return NotFound();
            }
            return Ok(villaToReturn);
        }

        [HttpPost("AddVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateVilla([FromBody] VillaDto villa)
        {
            // We can use use ModelState when we don't wanna use ApiController
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //CustomValidation
            if (VillaStore.VillaList.Select(villa => villa.Name).ToHashSet(StringComparer.OrdinalIgnoreCase).Contains(villa.Name))
            {
                ModelState.AddModelError("CustomError", "Villa name must be unique");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest(villa);
            }
            if (villa.Id != Guid.Empty)
            {
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError);
            }
            villa.Id = Guid.NewGuid();
            VillaStore.VillaList.Add(villa);

            return RedirectToAction("Success");

            return CreatedAtRoute(VillaDetailsRouteName, new { villa.Id }, villa);
        }

        [HttpDelete("DeleteVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public IActionResult DeleteVilla([FromQuery] Guid id, [FromQuery] string name = null)
        {
            VillaStore.VillaList.Remove(
                VillaStore.VillaList.SingleOrDefault(villa => villa.Id.Equals(id) || villa.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
            return NoContent();
        }

        [HttpPatch("UpdatePartiallyVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartiallyVilla([FromQuery] Guid id, JsonPatchDocument<VillaDto> patch)
        {
            if (patch is null || id == Guid.Empty)
            {
                return BadRequest();
            }
            var villaToUpdate = VillaStore.VillaList.SingleOrDefault(villa => villa.Id.Equals(id));
            if (villaToUpdate == null)
            {
                return NotFound();
            }
            patch.ApplyTo(villaToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
