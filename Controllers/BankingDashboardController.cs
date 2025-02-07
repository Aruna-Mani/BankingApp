using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Controllers
{
    public class BankingDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankingDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index action to show account details
        public async Task<IActionResult> Index()
        {
            var accounts = await _context.BankAccounts.ToListAsync();
            return View(accounts);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,AccountHolder,Balance")] BankAccount account)
        {
            if (ModelState.IsValid)
            {
                // Check if the AccountNumber already exists
                bool accountExists = await _context.BankAccounts
                    .AnyAsync(a => a.AccountNumber == account.AccountNumber);

                if (accountExists)
                {
                    ModelState.AddModelError("AccountNumber", "An account with this number already exists.");
                    return View(account); // Return to the view with the error message
                }
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.BankAccounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,AccountHolder,Balance")] BankAccount account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            return View(account);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.BankAccounts.FindAsync(id);
            _context.BankAccounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AccountExists(int id)
        {
            return _context.BankAccounts.Any(e => e.Id == id);
        }

    }
}
