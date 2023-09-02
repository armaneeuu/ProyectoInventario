using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoInventario.Data;
using ProyectoInventario.Models;

namespace ProyectoInventario.Controllers
{
    public class ObjetoDetectionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObjetoDetectionController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStockByImageName(string imageName)
        {
            String respuesta = "";
            // Mapeo de etiquetas a nombres de productos
            Dictionary<string, string> labelToProductNameMap = new Dictionary<string, string>
            {
                {"Plantilla blanca", "NombreProducto1"},
                {"Plantilla negra", "NombreProducto2"},
                {"Pasador blanco", "Pasador blanco"},
                {"Hilo blanco", "Hilo blanco"},
                // Agrega más elementos al mapeo según tus etiquetas y nombres de productos
            };

            respuesta=respuesta+"1<br>";

            if (labelToProductNameMap.ContainsKey(imageName))
            {
                respuesta=respuesta+"2<br>";
                string productName = labelToProductNameMap[imageName];
                var product = _context.DataProduct.FirstOrDefault(p => p.Nombre == productName);

                if (product != null)
                {
                    respuesta=respuesta+"3<br>";
                    return Json(new { Stock = product.Stock });
                }
            }
            respuesta=respuesta+"4<br>";
            return Json(new { Message = respuesta});
        }
    }
}
