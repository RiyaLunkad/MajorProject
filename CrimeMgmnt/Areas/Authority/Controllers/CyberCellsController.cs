using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrimeMgmnt.Data;
using CrimeMgmnt.Models;
using Microsoft.AspNetCore.Authorization;

namespace CrimeMgmnt.Areas.Authority.Controllers
{
    [Area("Authority")]
    [Authorize]
    public class CyberCellsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CyberCellsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET: Authority/CyberCells
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.cyberCells.Include(c => c.Users);
            return View(await applicationDbContext.ToListAsync());
        }

        
        // GET: Authority/CyberCells/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cyberCell = await _context.cyberCells
                .Include(c => c.Users)
                .FirstOrDefaultAsync(m => m.ControlRoomId == id);
            if (cyberCell == null)
            {
                return NotFound();
            }

            return View(cyberCell);
        }

        // GET: Authority/CyberCells/Create
        public IActionResult Create(int userid)
        {
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "UserId");
            CyberCell model=new CyberCell();
            model.UserId = userid;
            return View(model);
        }

        // POST: Authority/CyberCells/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ControlRoomId,UserId,IsEnabled,Status")] CyberCell cyberCell)
        {
            if (ModelState.IsValid)
            {
               // bool isDuplicateFound
                   // =_context.cyberCells.Any(c => CyberCell.UserId == Cybercell.userId);

                _context.Add(cyberCell);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "UserId", cyberCell.UserId);
            return View(cyberCell);
        }

        // GET: Authority/CyberCells/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cyberCell = await _context.cyberCells.FindAsync(id);
            if (cyberCell == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "UserId", cyberCell.UserId);
            return View(cyberCell);
        }

        // POST: Authority/CyberCells/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ControlRoomId,UserId,IsEnabled,Status")] CyberCell cyberCell)
        {
            if (id != cyberCell.ControlRoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cyberCell);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CyberCellExists(cyberCell.ControlRoomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "UserId", cyberCell.UserId);
            return View(cyberCell);
        }

        // GET: Authority/CyberCells/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cyberCell = await _context.cyberCells
                .Include(c => c.Users)
                .FirstOrDefaultAsync(m => m.ControlRoomId == id);
            if (cyberCell == null)
            {
                return NotFound();
            }

            return View(cyberCell);
        }

        // POST: Authority/CyberCells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cyberCell = await _context.cyberCells.FindAsync(id);
            _context.cyberCells.Remove(cyberCell);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CyberCellExists(int id)
        {
            return _context.cyberCells.Any(e => e.ControlRoomId == id);
        }

        public IActionResult GetReply()
        {
            return View("Create");
        }
    }
}
