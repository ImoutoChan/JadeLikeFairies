using System.Collections.Generic;
using System.Threading.Tasks;
using JadeLikeFairies.Services.Abstract;
using JadeLikeFairies.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JadeLikeFairies.Controllers
{
    [Route("api/[controller]")]
    public class NovelsController : Controller
    {
        private readonly INovelsService _novelsService;

        public NovelsController(INovelsService novelsService)
        {
            _novelsService = novelsService;
        }

        // GET api/novels
        [HttpGet]
        public async Task<List<NovelDto>> Get()
        {
            return await _novelsService.GetNovels();
        }

        // GET api/novels/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/novels
        [HttpPost]
        public void Post([FromBody]string value)
        {
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