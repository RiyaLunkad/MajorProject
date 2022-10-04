using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrimeMgmnt.Data;
using CrimeMgmnt.Models;
using Microsoft.Extensions.Logging;


namespace CrimeMgmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CyberCellsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CyberCellsController> _logger;

        public CyberCellsController(ApplicationDbContext context, ILogger<CyberCellsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/CyberCells

        [HttpGet]

        //since the expectation to controller is to return the package inside the Ok result method

        //public async Task<ActionResult<IEnumerable<CyberCell>>> GetcyberCells()
        public async Task<IActionResult> GetCyberCells()
        {
            //here we are packaging and telling to return the Ok result
            // return await _context.cyberCells.ToListAsync();

            try
            {
                var ControllRoom = await _context.cyberCells
                                            .Include(c => c.Users)
                                            .ToListAsync();

                if (ControllRoom == null)
                {
                    //_logger.LogWarning("No Id found in the database");
                    return NotFound();
                }
                // _logger.LogInformation("Extracted all the ControllRoom Id From Database");
                return Ok(ControllRoom);
            }
            catch (Exception exp)
            {
                // _logger.LogError("Its an attempt to Retrieve information from the database");
                return BadRequest(exp.Message);
            }
            //return Ok(await _context.cyberCells.ToListAsync());
        }

        // GET: api/CyberCells/5
        /// <summary>
        /// will search for id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>gets the user id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCyberCell(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var cyberCell = await _context.cyberCells.FindAsync(id);

                if (cyberCell == null)
                {
                    return NotFound();
                }
                return Ok(cyberCell);
                //return cyberCell;
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        // PUT: api/CyberCells/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Insert value of the id as proceesed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cyberCell"></param>
        /// <returns>returns status of user case</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCyberCell(int id, CyberCell cyberCell)
        {
            if (id != cyberCell.ControlRoomId)
            {
                return BadRequest();
            }

            _context.Entry(cyberCell).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CyberCellExists(id))
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

        // POST: api/CyberCells
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Helps in new creation
        /// </summary>
        /// <param name="cyberCell"></param>
        /// <returns>new user created</returns>
        [HttpPost]
        public async Task<IActionResult> PostCyberCell(CyberCell cyberCell)
        {
            

            if (!ModelState.IsValid)
            {

                try
                {

                    _context.cyberCells.Add(cyberCell);
                    await _context.SaveChangesAsync();

                    //int countaffected = await _context.SaveChangesAsync();
                    //if (countaffected > 0)

                    var result = CreatedAtAction("GetCyberCells", new { id = cyberCell.ControlRoomId }, cyberCell);
                    return Ok(result);
                }

                catch (System.Exception exp)
                {
                    ModelState.AddModelError("POST", exp.Message);
                }
            }
            return BadRequest(ModelState);



            // return CreatedAtAction("GetCyberCell", new { id = cyberCell.ControlRoomId }, cyberCell);
        }

        // DELETE: api/CyberCells/5
        /// <summary>
        /// Delete can be done only by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted id is removed</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCyberCell(int? id)
        {

            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {


                var cyberCell = await _context.cyberCells.FindAsync(id);
                if (cyberCell == null)
                {
                    return NotFound();
                }

                _context.cyberCells.Remove(cyberCell);
                await _context.SaveChangesAsync();

                return Ok(cyberCell);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }
        }
        private bool CyberCellExists(int id)
        {
            return _context.cyberCells.Any(e => e.ControlRoomId == id);
        }

    }
}

