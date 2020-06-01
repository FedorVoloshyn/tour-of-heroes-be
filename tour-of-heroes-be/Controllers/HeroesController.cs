using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tour_of_heroes_be.Models;
using tour_of_heroes_be.Services;

namespace tour_of_heroes_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        IHeroesService HeroService { get; }

        public HeroesController(IHeroesService heroService)
        {
            HeroService = heroService;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult> GetAllHeroes()
        {
            var result = await HeroService.GetHeroesAsync();

            return Ok(result);
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetHero(int id)
        {
            var result = await HeroService.GetHeroAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Heroes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(int id, Hero hero)
        {
            try
            {
                await HeroService.PutHeroAsync(hero);
            }
            catch(Exception ex)
            {
                var exmess = ex.Message;
                return StatusCode(500);
            }

            return Ok();
        }

        // POST: api/Heroes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            await HeroService.AddHeroAsync(hero);

            return Ok(hero.Id);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> DeleteHero(int id)
        {
            await HeroService.DeleteHeroAsync(id);

            return Ok();
        }
    }
}
