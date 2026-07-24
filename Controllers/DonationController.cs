using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodBankDB.EF;
using BloodBankDB.EF.Tables;

namespace BloodBankDB.Controllers
{
    public class DonationController : Controller
    {
        private readonly BloodBankDBContext db;

        public DonationController(BloodBankDBContext context)
        {
            db = context;
        }

        // GET: Donation
        public async Task<IActionResult> Index()
        {
            var data = db.Donations.Include(d => d.Donor);
            return View(await data.ToListAsync());
        }

        // GET: Donation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var donation = await db.Donations
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(x => x.DonationId == id);

            if (donation == null)
                return NotFound();

            return View(donation);
        }

        // GET: Donation/Create
        public IActionResult Create()
        {
            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "FullName");
            return View();
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("\n",
                    ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors.Select(e => x.Key + " : " + e.ErrorMessage)));

                return Content(errors);
            }

            try
            {
                db.Donations.Add(donation);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        // GET: Donation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var donation = await db.Donations.FindAsync(id);

            if (donation == null)
                return NotFound();

            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "FullName", donation.DonorId);

            return View(donation);
        }

        // POST: Donation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Donation donation)
        {
            if (id != donation.DonationId)
                return NotFound();

            if (ModelState.IsValid)
            {
                db.Update(donation);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.DonorId = new SelectList(db.Donors, "DonorId", "FullName", donation.DonorId);

            return View(donation);
        }

        // GET: Donation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var donation = await db.Donations
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(x => x.DonationId == id);

            if (donation == null)
                return NotFound();

            return View(donation);
        }

        // POST: Donation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await db.Donations.FindAsync(id);

            if (donation != null)
            {
                db.Donations.Remove(donation);
                await db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}