using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers
{
    public class LibroController : Controller
    {
        private readonly LibroService _libroService;
        private readonly IFileService _fileService;
        private readonly ICategoriaService _categoriaService;

        public LibroController(LibroService libroService, IFileService fileService, ICategoriaService categoriaService)
        {
            _libroService = libroService;
            _fileService = fileService;
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public IActionResult Add(Libro libro)
        {
            libro.CategoriasList = _categoriaService.List().
                Select( a=> new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            if (libro.ImageFile != null)
            {
                var resultado = _fileService.SaveImage(libro.ImageFile);

                if (resultado.Item1 == 0) {
                    TempData["msg"] = "La imagen no pudo guardarse: "+resultado.Item2;
                    return View(libro);
                }

                var imagenName = resultado.Item2;
                libro.Imagen = imagenName;
            }

            var resultadoLibro = _libroService.Add(libro);

            if (resultadoLibro) {
                TempData["msg"] = "Se agregó el libro con exito";
                return RedirectToAction(nameof(Add));
            }

            TempData["msg"] = "Error al agregar el libro";
            return View(libro);

        }

        public IActionResult Add()
        {

            var libro = new Libro();
            libro.CategoriasList = _categoriaService.List().
                Select(a => new SelectListItem { Text = a.Nombre, Value = a.Id.ToString() });

            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult LibroList()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(LibroList));
        }
    }
}
