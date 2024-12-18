using Geometrie.BLL;
using Geometrie.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Geometrie.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CercleController : ControllerBase
    {
        private readonly CercleService _service;

        public CercleController(CercleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllCercles());

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            var cercle = _service.GetCercleById(id);
            return cercle == null ? NotFound() : Ok(cercle);
        }

        [HttpPost]
        public IActionResult Add(Cercle cercle)
        {
            _service.AddCercle(cercle);
            return CreatedAtAction(nameof(GetById), new { id = cercle.Id }, cercle);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int id, Cercle cercle)
        {
            if (id != cercle.Id) return BadRequest();
            _service.UpdateCercle(cercle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.DeleteCercle(id);
            return NoContent();
        }
    }

}
