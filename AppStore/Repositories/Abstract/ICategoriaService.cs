using AppStore.Models.Domain;
using AppStore.Models.DTO;
namespace AppStore.Repositories.Abstract
{
    public interface ICategoriaService
    {
        bool Add(Categoria libro);
        bool Update(Categoria libro);
        Categoria GetById(int id);
        bool Delete(int id);
        IQueryable<Categoria> List();
        CategoriaListVm ListAll();
    }
}
