using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DragonNest.Models;
using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace DragonNest.Controllers
{
    [Produces("application/json")]
    [Route("api/DNClasses")]
    [EnableCors("AllowSpecificOrigins")]
    public class DNClassesController : Controller
    {
        private readonly DragonNestContext _context;

        public DNClassesController(DragonNestContext context)
        {
            _context = context;

            if (!_context.DNClasses.Any())
            {
                var dnClasses = new DNClass[]
                {
                    new DNClass
                    {
                        ClassName = "Kali",
                        ATKTYPE = MainATK.MAGICAL
                    },
                     new DNClass
                    {
                        ClassName = "Academic",
                        ATKTYPE = MainATK.MAGICAL
                    }
                };
                foreach (DNClass d in dnClasses)
                {
                    _context.DNClasses.Add(d);
                }
                _context.SaveChanges();

                var skills = new Skill[] {
                    new Skill
                    {
                        Name = "Alfredo",
                        DMG = 750,
                        Special = "Summon Alfredo",
                        DNClassID = _context.DNClasses.First(c => c.ClassName == "Academic").ClassName
                    },
                    new Skill
                    {
                        Name = "Wax",
                        DMG = 0,
                        Special = "Summon the Wax to slow enemy",
                        DNClassID = _context.DNClasses.First(c => c.ClassName == "Academic").ClassName
                    },
                    new Skill
                    {
                        Name = "Spirit of Genie",
                        DMG = 0,
                        Special = "Increase all the stats of allies for certain period of time by summoning the Spirit of Desert.",
                        DNClassID = _context.DNClasses.First(c => c.ClassName == "Kali").ClassName
                    },
                    new Skill
                    {
                        Name = "Phantom Guard",
                        DMG = 0,
                        Special = "Summons small spirit to protect oneself.",
                        DNClassID = _context.DNClasses.First(c => c.ClassName == "Kali").ClassName
                    }
                };

                foreach (Skill s in skills)
                {
                    _context.Skills.Add(s);
                }
                _context.SaveChanges();
            }



        }

        // GET: api/DNClasses
        [HttpGet]
        public async Task<List<DNClass>> GetDNClasses()
        {
          var dnClass = _context.DNClasses;
          foreach (DNClass c in dnClass)
            {
                c.Speak();
            }
          return await dnClass.ToListAsync();
        }

        // GET: api/DNClasses/5
        [HttpGet("{id}")]
        public IActionResult GetDNClass([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skills = _context.Skills.Include(s => s.DNClass).AsNoTracking().Where(c => c.DNClassID == id);

            var sk = (from c in _context.DNClasses
                      join s in _context.Skills
                      on c.ClassName equals s.DNClassID
                      where c.ClassName == id
                      select new
                      {
                          ID = s.ID,
                          Name = s.Name,
                          DMG = s.DMG,
                          Special = s.Special,
                          DNClassID = s.DNClassID
                      }).ToList();
            if (skills == null)
            {
                return NotFound();
            }

            return Ok(sk);
        }

        // PUT: api/DNClasses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDNClass([FromRoute] string id, [FromBody] DNClass dNClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dNClass.ClassName)
            {
                return BadRequest();
            }

            _context.Entry(dNClass).State = EntityState.Modified;

            try
            {
                var dn = _context.DNClasses.Find(id);
                if (dn == null)
                {
                    return NotFound();
                }

                dn.ATKTYPE = dNClass.ATKTYPE;
                _context.DNClasses.Update(dn);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DNClassExists(id))
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

        // POST: api/DNClasses
        [HttpPost]
        public async Task<IActionResult> PostDNClass([FromBody] DNClass dNClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DNClasses.Add(dNClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDNClass", new { id = dNClass.ClassName }, dNClass);
        }

        // DELETE: api/DNClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDNClass([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dNClass = await _context.DNClasses.SingleOrDefaultAsync(m => m.ClassName == id);
            if (dNClass == null)
            {
                return NotFound();
            }

            _context.DNClasses.Remove(dNClass);
            await _context.SaveChangesAsync();

            return Ok(dNClass);
        }

        private bool DNClassExists(string id)
        {
            return _context.DNClasses.Any(e => e.ClassName == id);
        }
    }
}