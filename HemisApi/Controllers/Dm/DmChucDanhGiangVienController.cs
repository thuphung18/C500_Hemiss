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
    public class DmChucDanhGiangVienController : ControllerBase
    {
        private readonly HemisContext _context;

        public DmChucDanhGiangVienController(HemisContext context)
        {
            _context = context;
        }

        // GET: api/DmChucDanhGiangVien
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DmChucDanhGiangVien>>> GetDmChucDanhGiangViens()
        {
            return await _context.DmChucDanhGiangViens.ToListAsync();
        }

        // GET: api/DmChucDanhGiangVien/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DmChucDanhGiangVien>> GetDmChucDanhGiangVien(int id)
        {
            var dmChucDanhGiangVien = await _context.DmChucDanhGiangViens.FindAsync(id);

            if (dmChucDanhGiangVien == null)
            {
                return NotFound();
            }

            return dmChucDanhGiangVien;
        }

        // PUT: api/DmChucDanhGiangVien/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmChucDanhGiangVien(int id, DmChucDanhGiangVien dmChucDanhGiangVien)
        {
            if (id != dmChucDanhGiangVien.IdChucDanhGiangVien)
            {
                return BadRequest();
            }

            _context.Entry(dmChucDanhGiangVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmChucDanhGiangVienExists(id))
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

        // POST: api/DmChucDanhGiangVien
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DmChucDanhGiangVien>> PostDmChucDanhGiangVien(DmChucDanhGiangVien dmChucDanhGiangVien)
        {
            _context.DmChucDanhGiangViens.Add(dmChucDanhGiangVien);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DmChucDanhGiangVienExists(dmChucDanhGiangVien.IdChucDanhGiangVien))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDmChucDanhGiangVien", new { id = dmChucDanhGiangVien.IdChucDanhGiangVien }, dmChucDanhGiangVien);
        }

        // DELETE: api/DmChucDanhGiangVien/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDmChucDanhGiangVien(int id)
        {
            var dmChucDanhGiangVien = await _context.DmChucDanhGiangViens.FindAsync(id);
            if (dmChucDanhGiangVien == null)
            {
                return NotFound();
            }

            _context.DmChucDanhGiangViens.Remove(dmChucDanhGiangVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DmChucDanhGiangVienExists(int id)
        {
            return _context.DmChucDanhGiangViens.Any(e => e.IdChucDanhGiangVien == id);
        }
    }
}
