using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webapplication1Mvc.models;

namespace Webapplication1Mvc.Controllers
{
    public class ProductGstdetailsController : Controller
    {
        private readonly Practice1Context _context;

        public ProductGstdetailsController(Practice1Context context)
        {
            _context = context;
        }

        // GET: ProductGstdetails
        public async Task<IActionResult> Index()
        {
            var practice1Context = _context.ProductGstdetails.Include(p => p.Product);
            return View(await practice1Context.ToListAsync());
        }

        // GET: ProductGstdetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGstdetail = await _context.ProductGstdetails
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGstdetail == null)
            {
                return NotFound();
            }

            return View(productGstdetail);
        }

        // GET: ProductGstdetails/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ProductGstdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductGstdetail productGstdetail)
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productGstdetail.ProductId);

            if (productGstdetail.ProductId.HasValue)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == productGstdetail.ProductId);

                if (product != null && product.Rate.HasValue && product.Category.HasValue)
                {
                    decimal gstPercent = product.Category.Value switch
                    {
                        0 => 20,
                        1 => 12,
                        2 => 18,
                        _ => 0
                    };

                    decimal totalGst = product.Rate.Value * gstPercent / 100;

                    productGstdetail.TotalGst = totalGst;
                    productGstdetail.Cgst = totalGst / 2;
                    productGstdetail.Sgst = totalGst / 2;
                }
            }

            // if GST values already generated and user clicks save
            if (productGstdetail.TotalGst.HasValue)
            {
                _context.ProductGstdetails.Add(productGstdetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productGstdetail);
        }
        // GET: ProductGstdetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGstdetail = await _context.ProductGstdetails.FindAsync(id);
            if (productGstdetail == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productGstdetail.ProductId);
            return View(productGstdetail);
        }

        // POST: ProductGstdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Sgst,Cgst,TotalGst")] ProductGstdetail productGstdetail)
        {
            if (id != productGstdetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productGstdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGstdetailExists(productGstdetail.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productGstdetail.ProductId);
            return View(productGstdetail);
        }

        // GET: ProductGstdetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGstdetail = await _context.ProductGstdetails
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGstdetail == null)
            {
                return NotFound();
            }

            return View(productGstdetail);
        }

        // POST: ProductGstdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productGstdetail = await _context.ProductGstdetails.FindAsync(id);
            if (productGstdetail != null)
            {
                _context.ProductGstdetails.Remove(productGstdetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductGstdetailExists(int id)
        {
            return _context.ProductGstdetails.Any(e => e.Id == id);
        }
    }
}
