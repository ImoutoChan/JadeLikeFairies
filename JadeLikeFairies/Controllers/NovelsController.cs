using System;
using System.Threading.Tasks;
using JadeLikeFairies.Services.Abstract;
using JadeLikeFairies.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JadeLikeFairies.Helpers;
using JadeLikeFairies.Services.Exceptions;

namespace JadeLikeFairies.Controllers
{
    [Route("api/[controller]")]
    public class NovelsController : Controller
    {
        private readonly INovelsService _novelsService;
        private readonly ILogger<NovelsController> _logger;

        public NovelsController(INovelsService novelsService, ILogger<NovelsController> logger)
        {
            _novelsService = novelsService;
            _logger = logger;
        }

        // GET api/novels
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var novels = await _novelsService.GetNovels();
                return Ok(novels);
            }
            catch (Exception e)
            {
                _logger.LogMethodError(e);

                return BadRequest(e.Message);
            }
        }

        // GET api/novels/5
        [HttpGet("{id}", Name = "GetNovel")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(id), "Id should be greater than 0.");
                }

                var novel = await _novelsService.GetNovel(id);

                if (novel == null)
                {
                    return NotFound();
                }

                return Ok(novel);
            }
            catch (Exception ex)
            {
                _logger.LogMethodError(ex);

                return BadRequest(ex.Message);
            }
        }

        // POST api/novels
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NovelPostDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                

                var newNovel = await _novelsService.AddNovel(value);

                return CreatedAtRoute("GetNovel", new {id = newNovel.Id}, newNovel);
            }
            catch (DeepValidationException e)
            {
                _logger.LogMethodError(e);

                ModelState.AddModelError(e.Key, e.Error);

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogMethodError(e);

                return BadRequest();
            }
        }

        // PATCH api/novels/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]NovelPatchDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(id), "Id should be greater than 0.");
                }

                var updatedNovel = await _novelsService.UpdateNovel(id, value);
                
                return AcceptedAtRoute("GetNovel", new { id = updatedNovel.Id }, updatedNovel);
            }
            catch (DeepValidationException e)
            {
                _logger.LogMethodError(e);

                ModelState.AddModelError(e.Key, e.Error);

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogMethodError(e);

                return BadRequest(e.Message);
            }
        }

        // DELETE api/novels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(id), "Id should be greater than 0.");
                }

                await _novelsService.Remove(id);

                return NoContent();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogMethodError(e);

                return BadRequest(e.Message);
            }
        }
    }
}