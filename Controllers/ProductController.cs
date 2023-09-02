using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Data;
using ProyectoInventario.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoInventario.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public IActionResult Index()
        {
              var lista = _context.DataProduct.Where(x=>x.Categoria == "I");
            return View(lista);
        }
        public IActionResult Index2()
        {
              var lista = _context.DataProduct.Where(x=>x.Categoria == "C");
            return View(lista);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.DataProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Index(String Searchpro)
        {
            ViewData["Getemployeedetails"] = Searchpro;
            var empquery = from x in _context.DataProduct select x;
            if (!string.IsNullOrEmpty(Searchpro))
            {
                empquery = empquery.Where(x => x.Nombre.Contains(Searchpro));
            }
            empquery = empquery.Where(x=>x.Categoria == "I");
            return View(await empquery.AsNoTracking().ToListAsync());

        }

        [HttpGet]
        public async Task<IActionResult> Index2(String Searchpro)
        {
            ViewData["Getemployeedetails2"] = Searchpro;
            var empquery = from x in _context.DataProduct select x;
            if (!string.IsNullOrEmpty(Searchpro))
            {
                empquery = empquery.Where(x => x.Nombre.Contains(Searchpro));
            }
            empquery = empquery.Where(x=>x.Categoria == "C");
            return View(await empquery.AsNoTracking().ToListAsync());

        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, List<IFormFile> upload)
        {
            if (ModelState.IsValid)
            {
                if (upload.Count > 0)
                {

                    foreach (var up in upload)
                    {
                        Stream str = up.OpenReadStream();
                        BinaryReader br = new BinaryReader(str);
                        Byte[] fileDet = br.ReadBytes((Int32)str.Length);
                        product.Imagen = fileDet;
                        product.ImagenName = Path.GetFileName(up.FileName);
                    }
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.DataProduct.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, List<IFormFile> upload)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (upload == null || upload.Count <= 0)
                    {
                        byte[] imagen = product.Imagen;
                        var nom = product.ImagenName;
                        product.Imagen = imagen;
                        product.ImagenName = nom;
                    }
                    else
                    {
                        foreach (var up in upload)
                        {
                            Stream str = up.OpenReadStream();
                            BinaryReader br = new BinaryReader(str);
                            Byte[] fileDet = br.ReadBytes((Int32)str.Length);
                            product.Imagen = fileDet;
                            product.ImagenName = Path.GetFileName(up.FileName);
                        }
                    }
                    ModelState.AddModelError("precio", "Solo valores numericos");
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.DataProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.DataProduct.FindAsync(id);
            product.Status = "ELIMINADO";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.DataProduct.Any(e => e.Id == id);
        }
        public IActionResult MostrarImagen(int id)
        {
            var producto = _context.DataProduct.Find(id);
            byte[] imagen = producto.Imagen;
            return File(imagen, "img/png");
        }
    }
}
