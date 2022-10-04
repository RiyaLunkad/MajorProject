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
    
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Authority/Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.users.ToListAsync());
        }

        // GET: Authority/Users/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Authority/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authority/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("UserId,UserName,Age,CrimePlatform,CrimeDescription,DateTime,Place,mobile")] User user)
        
        {
            if (ModelState.IsValid)
            {
                user.mobile = user.mobile.Trim();
                user.UserName = user.UserName.Trim();

                bool ifduplicatefound2 = _context.users.Any(m => m.UserName == user.UserName);
                bool ifduplicatefound1 = _context.users.Any(m => m.mobile == user.mobile);
                if (ifduplicatefound1)
                {
                    ModelState.AddModelError("mobile", "This mobile Number Already exsist!");
                }
                else if (ifduplicatefound2)
                {

                    ModelState.AddModelError("UserName", "This UserName Already exsist!");
                }
                else
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = user.UserId });
                }

            }
            return View(user);
        }
   

       /* public async Task<IActionResult> Create([Bind("UserId,UserName,Age,CrimePlatform,CrimeDescription,DateTime,Place,mobile")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View("Details", user);
        }*/
       

        // GET: Authority/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Authority/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Age,CrimePlatform,CrimeDescription,DateTime,Place,mobile")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Authority/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Authority/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.users.FindAsync(id);
            _context.users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.UserId == id);
        }

        public void Reply()
        {
            CyberCellsController cyberCellsController = new CyberCellsController(_context);

            cyberCellsController.GetReply();

            //return View("");
        }
    }
}
