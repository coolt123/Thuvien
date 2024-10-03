using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuvienMvc.Models;

namespace ThuvienMvc.Controllers
{
    public class AdminController1 : Controller
    {
        private readonly Data _context;
        public AdminController1(Data data) 
        {
            _context = data;
        }

        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Createdat = DateTime.Now;
                admin.Updatedat = DateTime.Now;
                admin.deleteflag = false;

                _context.admins.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Admin admin)
        {
            if (id != admin.IdAmin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    admin.Updatedat = DateTime.Now;
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.admins
                .FirstOrDefaultAsync(e => e.IdAmin == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.admins.FindAsync(id);
            _context.admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
