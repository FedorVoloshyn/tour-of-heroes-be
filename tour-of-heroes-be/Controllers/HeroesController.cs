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
        private readonly HeroContext _context;
        IHeroesService HeroService { get; }

        public HeroesController(HeroContext context, IHeroesService heroService)
        {
            _context = context;
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
            if (id != hero.Id)
            {
                return BadRequest();
            }

            _context.Entry(hero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Heroes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
            return CreatedAtAction(nameof(GetHero), new { id = hero.Id }, hero);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> DeleteHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return hero;
        }

        private bool HeroExists(int id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }
    }
}
