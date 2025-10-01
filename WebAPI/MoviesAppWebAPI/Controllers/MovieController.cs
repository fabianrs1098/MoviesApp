using Microsoft.AspNetCore.Mvc;
using MoviesAppWebAPI.Data;
using MoviesAppWebAPI.Models;

namespace MoviesAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieData _movieData;
        public MovieController(MovieData movieData)
        {
            _movieData = movieData;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Movie> Lista = await _movieData.Get();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Movie objeto = await _movieData.GetById(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movie objeto)
        {
            bool respuesta = await _movieData.Create(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Movie objeto)
        {
            bool respuesta = await _movieData.Update(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool respuesta = await _movieData.Delete(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
