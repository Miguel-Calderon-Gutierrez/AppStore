using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public IActionResult Add(CategoriaListVm categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var newCategoria = new Categoria()
            {
                Nombre = categoria.Nombre
            };

            var resultadoLibro = _categoriaService.Add(newCategoria);

            if (resultadoLibro)
            {
                TempData["msg"] = "Se agregó la categoria con exito";
                return RedirectToAction(nameof(Add));
            }

            TempData["msg"] = "Error al agregar la categoria";
            return View(categoria);

        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var categoria = _categoriaService.GetById(id);
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {

            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var resultadoCategoria = _categoriaService.Update(categoria);

            if (!resultadoCategoria)
            {
                TempData["msg"] = "Error al actualizar la categoria";
                return View(categoria);
            }


            TempData["msg"] = "Se actualizó con exito";
            return View(categoria);
        }

        public IActionResult CategoriaList()
        {
            var categorias = _categoriaService.ListAll();

            return View(categorias);
        }

        public IActionResult Delete(int id)
        {
            _categoriaService.Delete(id);
            return RedirectToAction(nameof(CategoriaList));
        }
    }
}