using JediApp.Database.Domain;
using JediApp.Database.Repositories;
using JediApp.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace JediApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model =_userRepository.GetAllUsers();

            return View(model);
        }

        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _userRepository.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userRepository.Users.FirstOrDefaultAsync(x=>x.Id==id);
        //        //.Include(x => x.Id)
        //        //.FirstOrDefaultAsync(x=>x.Id == id);
                
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _userRepository.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userRepository.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = new SelectList(_userRepository.Set<User>(), "Id", "Id", user.Id);
        //    return View(user);
        //}
       
        //public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName")] User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _userRepository.Update(user);
        //            await _userRepository.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UsersExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_userRepository.Set<User>(), "Id", "Id", user.Id);
        //    return View(user);
        //}

        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _userRepository.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userRepository.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_dbContext.Users == null)
        //    {
        //        return Problem("Entity set 'JediAppDbContextConnection.User'  is null.");
        //    }
        //    var user = await _dbContext.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _dbContext.Users.Remove(user);
        //    }

        //    await _dbContext.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UsersExists(string id)
        //{
        //    return _userRepository.Users.Any(e => e.Id == id);
        //}

    }
}
