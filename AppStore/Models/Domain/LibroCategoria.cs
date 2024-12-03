using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.Domain
{
    public class LibroCategoria
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public int LibroId { get; set; }
        public Libro? Libro { get; set; }

    }
}
