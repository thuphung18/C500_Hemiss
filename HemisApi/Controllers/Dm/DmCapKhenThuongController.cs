using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using C500Hemis;

namespace HemisApi.Controllers.Dm
{
    [Route("api/[controller]")]
    [ApiController]
    public class DmCapKhenThuongController : ControllerBase
    {
        private readonly HemisContext _context;

        public DmCapKhenThuongController(HemisContext context)
        {
            _context = context;
        }

        // GET: api/DmCapKhenThuong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DmCapKhenThuong>>> GetDmCapKhenThuongs()
        {
            return await _context.DmCapKhenThuongs.ToListAsync();
        }

        // GET: api/DmCapKhenThuong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DmCapKhenThuong>> GetDmCapKhenThuong(int id)
        {
            var dmCapKhenThuong = await _context.DmCapKhenThuongs.FindAsync(id);

            if (dmCapKhenThuong == null)
            {
                return NotFound();
            }

            return dmCapKhenThuong;
        }

        // PUT: api/DmCapKhenThuong/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmCapKhenThuong(int id, DmCapKhenThuong dmCapKhenThuong)
        {
            if (id != dmCapKhenThuong.IdCapKhenThuong)
            {
                return BadRequest();
            }

            _context.Entry(dmCapKhenThuong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmCapKhenThuongExists(id))
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

        // POST: api/DmCapKhenThuong
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DmCapKhenThuong>> PostDmCapKhenThuong(DmCapKhenThuong dmCapKhenThuong)
        {
            _context.DmCapKhenThuongs.Add(dmCapKhenThuong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DmCapKhenThuongExists(dmCapKhenThuong.IdCapKhenThuong))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDmCapKhenThuong", new { id = dmCapKhenThuong.IdCapKhenThuong }, dmCapKhenThuong);
        }

        // DELETE: api/DmCapKhenThuong/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDmCapKhenThuong(int id)
        {
            var dmCapKhenThuong = await _context.DmCapKhenThuongs.FindAsync(id);
            if (dmCapKhenThuong == null)
            {
                return NotFound();
            }

            _context.DmCapKhenThuongs.Remove(dmCapKhenThuong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DmCapKhenThuongExists(int id)
        {
            return _context.DmCapKhenThuongs.Any(e => e.IdCapKhenThuong == id);
        }
    }
}
