using Grad.Models;
using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePlacesController : ControllerBase
    {
        private IRepoBase<Type_place> _TypeRepo;

        public TypePlacesController(IRepoBase<Type_place> TypeRepo)
        {
            _TypeRepo = TypeRepo;
        }

        [HttpPost("Create Type_Place")]
        public async Task<IActionResult> Create( Type_place Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            Type.Name = Type.Name.ToUpper();
            _TypeRepo.CreateAsync(Type);
            return Ok("Type Place added successfully.");
        }

        // Return all Types of  Tourism place
        [HttpGet("Get All Types of tourist places")]
        public async Task<IActionResult> GetAllTypePlaces()
        {
            var Types = await _TypeRepo.GetAsyncAll();
            return Ok(Types);
        }

        // Returns a  Type Tourism place through id
        [HttpGet("Get Type of Tourism place ById")]
        public async Task<IActionResult> GetById(int id)
        {
            Type_place? Types = await _TypeRepo.GetByIdAsync(id);
            if (Types == null)
            {
                return Ok("Type Tourism place not found");
            }

            return Ok(Types);
        }
      

        [HttpPost("Update Type Place")]
        public async Task<IActionResult> UpdateTypePlace(Type_place typePlace)
        {
            int id = (int)(HttpContext.Session.GetInt32("TypePlaceId"));
            Type_place? oldTypePlace = await _TypeRepo.GetByIdAsync(id);
            if (oldTypePlace == null)
            {
                return Ok("Type Place not found");
            }
            oldTypePlace.Name = typePlace.Name.ToUpper();
            await _TypeRepo.UpdateAsync(oldTypePlace);
            HttpContext.Session.Remove("TypePlaceId");

            return Ok("Type Place updated successfully");
        }


        [HttpDelete("Delete Type ById ")]
        public async Task<IActionResult> Deletetype(int type)
        {
            try
            {

                await _TypeRepo.DeleteAsync(type);
                return Ok("Deleted");
            }
            catch
            {
                return Ok("Canot Delete This type ");
            }
        }
    }
}
