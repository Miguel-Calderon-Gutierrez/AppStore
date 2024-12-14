using AppStore.Models.Domain;
using AppStore.Models.Domain.Context;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DatabaseContext _DbContext;

        public CategoriaService(DatabaseContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IQueryable<Categoria> List()
        {
            return _DbContext.Categorias!.AsQueryable();
        }
    }
}
