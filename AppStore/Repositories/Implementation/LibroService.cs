﻿using AppStore.Models.Domain;
using AppStore.Models.Domain.Context;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class LibroService : ILibroService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IFileService _fileService;

        public LibroService(DatabaseContext dbContext ,IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public bool Add(Libro libro)
        {
            try
            {
                _dbContext.Libros!.Add(libro);
                _dbContext.SaveChanges();

                foreach (int categoriaId in libro.Categorias)
                {
                    var LibroCategoria = new LibroCategoria
                    {
                        LibroId = libro.Id,
                        CategoriaId = categoriaId
                    };

                    _dbContext.LibroCategorias!.Add(LibroCategoria);
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var libro = GetById(id);
                if (libro == null)
                {
                    return false;
                }

                var libroCategorias = _dbContext.LibroCategorias!.Where(categoria => categoria.LibroId == libro.Id);
                _dbContext.LibroCategorias!.RemoveRange(libroCategorias);

                _fileService.DeleteImage(libro.Imagen!);

                _dbContext.Libros!.Remove(libro);

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Libro GetById(int id)
        {
            var libro = _dbContext.Libros!.Find(id)!;
            var categorias = (
                   from categoria in _dbContext.Categorias
                   join lc in _dbContext.LibroCategorias!
                   on categoria.Id equals lc.CategoriaId
                   where lc.LibroId == libro.Id
                   select categoria.Nombre
                   ).ToList();

            var categoriaNombres = string.Join(",", categorias);
            libro.CategoriasNames = categoriaNombres;

            return libro;
        }

        public LibroListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new LibroListVm();
            var list = _dbContext.Libros!.ToList();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Titulo!.ToLower().Contains(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int totalPages = (int)Math.Ceiling(count / (double)pageSize);
                list.Skip((currentPage - 1) * pageSize).Take(pageSize);
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPage = totalPages;
            }

            foreach (var libro in list)
            {
                var categorias = (
                    from categoria in _dbContext.Categorias
                    join lc in _dbContext.LibroCategorias
                    on categoria.Id equals lc.CategoriaId
                    where lc.LibroId == libro.Id
                    select categoria.Nombre
                    ).ToList();

                var categoriaNombres = string.Join(",", categorias);
                libro.CategoriasNames = categoriaNombres;

            }

            data.LibroList = list.AsQueryable();

            return data;
        }

        public bool Update(Libro libro)
        {
            try
            {
                var categoriasParaEliminar = _dbContext.LibroCategorias!.Where(a => a.LibroId == libro.Id);
                _dbContext.LibroCategorias!.RemoveRange(categoriasParaEliminar);
                _dbContext.SaveChanges();

                foreach (var categoriaId in libro.Categorias!)
                {
                    var libroCategoria = new LibroCategoria { CategoriaId = categoriaId, LibroId = libro.Id };
                    _dbContext.LibroCategorias!.Add(libroCategoria);
                }

                _dbContext.Libros!.Update(libro);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetCategoriaByLibroId(int LibroId)
        {
            return _dbContext.LibroCategorias!.Where(a => a.LibroId == LibroId).Select(a => a.CategoriaId).ToList();
        }
    }
}
