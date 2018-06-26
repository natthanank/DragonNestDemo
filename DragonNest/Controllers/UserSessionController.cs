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
using DragonNest.Data;

namespace DragonNest.Controllers
{
    [Produces("application/json")]
    [Route("api/UserSessions")]
    [EnableCors("AllowSpecificOrigins")]
    public class UserSessionController : Controller
    {
        private readonly UserSessionContext _context;

        public UserSessionController(UserSessionContext context)
        {
            _context = context;

            if (!_context.UserSessions.Any())
            {
                _context.UserSessions.Add(new UserSession { UserID = 1, SessionID = 1 });
                _context.SaveChanges();
            }



        }

        // GET: api/DNClasses
        [HttpGet]
        public async Task<List<UserSession>> GetUserSessions()
        {
           
            return await _context.UserSessions.ToListAsync();
        }

        //// GET: api/DNClasses/5
        //[HttpGet("{id}")]
        //public IActionResult GetDNClass([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var skills = _context.Skills.Include(s => s.DNClass).AsNoTracking().Where(c => c.DNClassID == id);

        //    var sk = (from c in _context.DNClasses
        //              join s in _context.Skills
        //              on c.ClassName equals s.DNClassID
        //              where c.ClassName == id
        //              select new
        //              {
        //                  ID = s.ID,
        //                  Name = s.Name,
        //                  DMG = s.DMG,
        //                  Special = s.Special,
        //                  DNClassID = s.DNClassID
        //              }).ToList();
        //    if (skills == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(sk);
        //}

        //// PUT: api/DNClasses/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDNClass([FromRoute] string id, [FromBody] DNClass dNClass)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != dNClass.ClassName)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dNClass).State = EntityState.Modified;

        //    try
        //    {
        //        var dn = _context.DNClasses.Find(id);
        //        if (dn == null)
        //        {
        //            return NotFound();
        //        }

        //        dn.ATKTYPE = dNClass.ATKTYPE;
        //        _context.DNClasses.Update(dn);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DNClassExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/DNClasses
        [HttpPost]
        public async Task<IActionResult> PostUserSession([FromBody] UserSession userSession)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                _context.UserSessions.Add(userSession);
                await _context.SaveChangesAsync();
            } catch (Exception e)
            {
               
            }



            return Ok();
        }

        //// DELETE: api/DNClasses/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDNClass([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var dNClass = await _context.DNClasses.SingleOrDefaultAsync(m => m.ClassName == id);
        //    if (dNClass == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.DNClasses.Remove(dNClass);
        //    await _context.SaveChangesAsync();

        //    return Ok(dNClass);
        //}

        //private bool DNClassExists(string id)
        //{
        //    return _context.DNClasses.Any(e => e.ClassName == id);
        //}
    }

    
}
