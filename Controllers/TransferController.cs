using BankingApp.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Controllers
{
    //[Authorize]
    public class TransferController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransferController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FundTransfers
        public async Task<IActionResult> Index()
        {
            var transfers = await _context.FundTransfers.ToListAsync();
            return View(transfers);  // Displays all fund transfers
        }

        // GET: FundTransfers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FundTransfers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SenderAccount,ReceiverAccount,Amount,Description")] FundTransfer fundTransfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fundTransfer);  // Add the new fund transfer
                await _context.SaveChangesAsync();  // Save to database
                return RedirectToAction(nameof(Index));
            }
            return View(fundTransfer);
        }

        // GET: FundTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundTransfer = await _context.FundTransfers.FindAsync(id);
            if (fundTransfer == null)
            {
                return NotFound();
            }
            return View(fundTransfer);
        }

        // POST: FundTransfers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SenderAccount,ReceiverAccount,Amount,Description")] FundTransfer fundTransfer)
        {
            if (id != fundTransfer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fundTransfer);  // Update the existing transfer
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.FundTransfers.Any(e => e.Id == fundTransfer.Id))
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
            return View(fundTransfer);
        }

        // GET: FundTransfers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var fundTransfer = await _context.FundTransfers
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (fundTransfer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(fundTransfer);
        //}

        // POST: FundTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fundTransfer = await _context.FundTransfers.FindAsync(id);
            _context.FundTransfers.Remove(fundTransfer);  // Remove the transfer from the database
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
