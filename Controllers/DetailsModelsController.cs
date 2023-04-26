using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DetailsModelsController : Controller
    {
        private readonly WebApplication1Context _context;

        public DetailsModelsController(WebApplication1Context context)
        {
            _context = context;
        }

        public ActionResult _Menu()
        {
            return PartialView();
        }

        // GET: DetailsModels
        public async Task<IActionResult> Index()
        {
            //return   _context.DetailsModel != null ? 
            //               View(await _context.DetailsModel.ToListAsync()) :
            //               Problem("Entity set 'WebApplication1Context.DetailsModel'  is null.");
            ViewBag.Message = "Hello , This is about viewBag";
            List<DetailsModel> list = _context.DetailsModel.ToList();
            ViewBag.list = list;
            return View(list);

        }

        // GET: DetailsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetailsModel == null)
            {
                return NotFound();
            }

            var model = await _context.DetailsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Employee = model;
            ViewBag.PageTitle = "Details";

            return View();
        }

        // GET: DetailsModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] DetailsModel detailsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detailsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detailsModel);
        }

        // GET: DetailsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetailsModel == null)
            {
                return NotFound();
            }

            var detailsModel = await _context.DetailsModel.FindAsync(id);
            if (detailsModel == null)
            {
                return NotFound();
            }
            return View(detailsModel);
        }

        // POST: DetailsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] DetailsModel detailsModel)
        {
            if (id != detailsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detailsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailsModelExists(detailsModel.Id))
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
            return View(detailsModel);
        }

        // GET: DetailsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetailsModel == null)
            {
                return NotFound();
            }

            var detailsModel = await _context.DetailsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailsModel == null)
            {
                return NotFound();
            }

            return View(detailsModel);
        }

        // POST: DetailsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetailsModel == null)
            {
                return Problem("Entity set 'WebApplication1Context.DetailsModel'  is null.");
            }
            var detailsModel = await _context.DetailsModel.FindAsync(id);
            if (detailsModel != null)
            {
                _context.DetailsModel.Remove(detailsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailsModelExists(int id)
        {
          return (_context.DetailsModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
