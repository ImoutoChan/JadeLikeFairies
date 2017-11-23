using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JadeLikeFairies.Services.Abstract;
using JadeLikeFairies.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JadeLikeFairies.Helpers;

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
        public async Task<List<NovelDto>> Get()
        {
            return await _novelsService.GetNovels();
        }

        // GET api/novels/5
        [HttpGet("{id}", Name = "GetNovel")]
        public async Task<IActionResult> Get(int id)
        {
            var novel = await _novelsService.GetNovel(id);

            if (novel == null)
            {
                return NotFound();
            }

            return Ok(novel);
        }

        // POST api/novels
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NovelPostDto value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest();
                }

                var newNovel = await _novelsService.AddNovel(value);

                return CreatedAtRoute("GetNovel", new {id = newNovel.Id}, newNovel);
            }
            catch (ValidationException e)
            {
                _logger.LogMethodError(e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogMethodError(e);

                return BadRequest();
            }
        }

        // PUT api/novels/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}