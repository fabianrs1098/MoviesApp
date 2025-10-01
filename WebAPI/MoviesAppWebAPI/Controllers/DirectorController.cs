using Microsoft.AspNetCore.Mvc;
using MoviesAppWebAPI.Data;
using MoviesAppWebAPI.Models;

namespace MoviesAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly DirectorData _directorData;
        public DirectorController(DirectorData directorData)
        {
            _directorData = directorData;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Director> Lista = await _directorData.Get();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Director objeto = await _directorData.GetById(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Director objeto)
        {
            bool respuesta = await _directorData.Create(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Director objeto)
        {
            bool respuesta = await _directorData.Update(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool respuesta = await _directorData.Delete(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
