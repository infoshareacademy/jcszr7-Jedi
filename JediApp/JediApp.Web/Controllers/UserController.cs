using JediApp.Database.Domain;
using JediApp.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace JediApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly JediAppDbContext _dbContext;

        public UserController(JediAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var db = _dbContext.Users;
            return View(await db.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id==id);
                //.Include(x => x.Id)
                //.FirstOrDefaultAsync(x=>x.Id == id);
                
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_dbContext.Set<User>(), "Id", "Id", user.Id);
            return View(user);
        }
       
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(user.Id))
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
            ViewData["UserId"] = new SelectList(_dbContext.Set<User>(), "Id", "Id", user.Id);
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_dbContext.Users == null)
            {
                return Problem("Entity set 'WebMvcDbHouseBillsWebMvcContext.Bill'  is null.");
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(string id)
        {
            return _dbContext.Users.Any(e => e.Id == id);
        }

    }
}
