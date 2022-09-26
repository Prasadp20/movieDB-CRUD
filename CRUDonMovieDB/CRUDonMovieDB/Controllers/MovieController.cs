using CRUDonMovieDB.Model;
using CRUDonMovieDB.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDonMovieDB.Controllers
{
    [Route("api/Movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        
        private readonly ILogger _log;
        public IMovieRepository _mov;

        public MovieController(IConfiguration _config ,ILoggerFactory logfact, IMovieRepository mov)
        {
            _log = logfact.CreateLogger<MovieController>();
            _mov = mov;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(BaseModel.DeleteObj obj)
        {
            try
            {
                var res = await _mov.DeleteMovie(obj);
                if(res == 0)
                    return NotFound();

                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMovies(string? searchtxt, int? Id)
        {
            try
            {
                var res = await _mov.GetAllMovies(searchtxt, Id);   
                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("MovieById")]
        public async Task<IActionResult> GetMovie(int Id)
        {
            try
            {
                var res = await _mov.GetMovie(Id);
                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(5600, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieDto move)
        {
            try
            {
                var res = await _mov.CreateMovie(move);
                if (res == null)
                    return NotFound();
                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(UpdateMovieDto move)
        {
            try
            {
                var res = await _mov.UpdateMovie(move);
                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
