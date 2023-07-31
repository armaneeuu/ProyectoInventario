using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoInventario.Models;
using ProyectoInventario.Data;

namespace ProyectoInventario.Controllers
{
    public class CatalogoController:Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CatalogoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var productos = from o in _context.DataProduct select o;
            return View(await productos.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Index(String Empsearch)
        {
            ViewData["Getemployeedetails"] = Empsearch;
            var empquery = from x in _context.DataProduct select x;
            if (!string.IsNullOrEmpty(Empsearch))
            {
                empquery = empquery.Where(x => x.Nombre.Contains(Empsearch));
            }
            return View(await empquery.AsNoTracking().ToListAsync());

        }
        public async Task<IActionResult> Details(int? id)
        {
            Product objProduct = await _context.DataProduct.FindAsync(id);
            if (objProduct == null)
            {
                return NotFound();
            }
            return View(objProduct);
        }
        public IActionResult MostrarImagen(int id)
        {
            var producto = _context.DataProduct.Find(id);
            byte[] imagen = producto.Imagen;
            return File(imagen, "img/png");
        }

    }
}