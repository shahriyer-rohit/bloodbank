using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodBankDB.EF;
using BloodBankDB.EF.Tables;
using BloodBankDB.Models;

namespace BloodBankDB.Controllers
{
    public class DonorController : Controller
    {
        private readonly BloodBankDBContext db;

        public DonorController(BloodBankDBContext context)
        {
            db = context;
        }

        // ===================== CRUD =====================

        // GET: Donor
        public async Task<IActionResult> Index()
        {
            return View(await db.Donors.ToListAsync());
        }

        // GET: Donor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var donor = await db.Donors.FirstOrDefaultAsync(x => x.DonorId == id);

            if (donor == null)
                return NotFound();

            return View(donor);
        }

        // GET: Donor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Add(donor);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(donor);
        }

        // GET: Donor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var donor = await db.Donors.FindAsync(id);

            if (donor == null)
                return NotFound();

            return View(donor);
        }

        // POST: Donor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Donor donor)
        {
            if (id != donor.DonorId)
                return NotFound();

            if (ModelState.IsValid)
            {
                db.Update(donor);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(donor);
        }

        // GET: Donor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var donor = await db.Donors.FirstOrDefaultAsync(x => x.DonorId == id);

            if (donor == null)
                return NotFound();

            return View(donor);
        }

        // POST: Donor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donor = await db.Donors.FindAsync(id);

            if (donor != null)
            {
                db.Donors.Remove(donor);
                await db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ===================== LINQ =====================

        // Filter by Blood Group
        public IActionResult Filter(string bloodGroup)
        {
            var donors = db.Donors.AsQueryable();

            if (!string.IsNullOrEmpty(bloodGroup))
            {
                donors = donors.Where(d => d.BloodGroup == bloodGroup);
            }

            return View(donors.ToList());
        }

        // Sort by Last Donation Date
        public IActionResult SortByLastDonation()
        {
            var donors = db.Donors
                .OrderByDescending(d => d.LastDonationDate)
                .ToList();

            return View(donors);
        }

        // Donor Donation Count
        public IActionResult DonorDonationCount()
        {
            var data = db.Donors
                .Select(d => new DonorDonationCountVM
                {
                    DonorId = d.DonorId,
                    FullName = d.FullName,
                    BloodGroup = d.BloodGroup,
                    TotalDonations = d.Donations.Count()
                })
                .ToList();

            return View(data);
        }

        // Total Blood Volume
        public IActionResult TotalBloodVolume()
        {
            var totalVolume = db.Donations.Sum(d => d.VolumeMl);

            ViewBag.TotalVolume = totalVolume;

            return View();
        }
    }
}