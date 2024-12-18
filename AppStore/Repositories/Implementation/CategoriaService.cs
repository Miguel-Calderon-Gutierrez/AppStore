using AppStore.Models.Domain;
using AppStore.Models.Domain.Context;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Repositories.Implementation
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DatabaseContext _dbContext;

        public CategoriaService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Categoria categoria)
        {
            try
            {
                _dbContext.Categorias!.Add(categoria);
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
                var categoria = GetById(id);
                if (categoria == null)
                {
                    return false;
                }

                var libroCategorias = _dbContext.LibroCategorias!.Where(libroCategoria => libroCategoria.CategoriaId == categoria.Id);
                _dbContext.LibroCategorias!.RemoveRange(libroCategorias);
                _dbContext.Categorias!.Remove(categoria);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Categoria GetById(int id)
        {
            return _dbContext.Categorias!.Find(id)!;
        }

        public bool Update(Categoria categoria)
        {
            try
            {            

                _dbContext.Categorias!.Update(categoria);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<Categoria> List()
        {
            return _dbContext.Categorias!.AsQueryable();
        }

        public CategoriaListVm ListAll()
        {
            CategoriaListVm data = new CategoriaListVm();
            data.CategoriaList = _dbContext.Categorias!.ToList().AsQueryable();

            return data;
        }
    }
}
