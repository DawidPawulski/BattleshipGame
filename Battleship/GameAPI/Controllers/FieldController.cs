using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPI.Controllers.Interfaces;
using GameAPI.Data;
using GameAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class FieldController : ControllerBase, IFieldController
    {
        private DataContext _context;

        public FieldController(DataContext context)
        {
            _context = context;
        }
        
        // GET: api/field
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Field>>> GetFields()
        {
            return await _context.Fields.ToListAsync();
        }
        
        // GET: api/field/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> GetField(int id)
        {
            var field = await _context.Fields.FindAsync(id);

            if (field == null)
            {
                return NotFound();
            }

            return field;
        }
        
        // PUT: api/field/1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutField(int id, Field field)
        {
            if (id != field.Id)
            {
                return BadRequest();
            }

            _context.Entry(field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
        
        // POST: api/field
        [HttpPost]
        public async Task<ActionResult<Field>> PostField(Field field)
        {
            await _context.Fields.AddAsync(field);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetField", new {id = field.Id}, field);
        }
        
        // DELETE: api/field/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Field>> DeleteField(int id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();

            return field;
        }

        private bool FieldExists(int id)
        {
            return _context.Fields.Any(f => f.Id == id);
        }
    }
}