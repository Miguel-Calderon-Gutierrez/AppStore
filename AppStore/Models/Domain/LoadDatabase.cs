using AppStore.Models.Domain.Context;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain
{
    public class LoadDatabase
    {
        public static async Task InsertarData(  DatabaseContext context, 
                                                UserManager<ApplicationUser> userManager,
                                                RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any()) {
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }

            if (!userManager.Users.Any()) {
                var usuario = new ApplicationUser { 
                    Nombre = "Miguel",
                    Email = "miguelcalderon.dev@gmail.com",
                    UserName = "miguel.calderon"
                };

                await userManager.CreateAsync(usuario, "Mac2806$$$");
                await userManager.AddToRoleAsync(usuario, "ADMIN");
            }

            if (!context.Categorias!.Any())
            {
                context.AddRange(
                new Categoria {Nombre = "Drama" },
                new Categoria {Nombre = "Comedia" },
                new Categoria {Nombre = "Accion" },
                new Categoria {Nombre = "Terror" },
                new Categoria {Nombre = "Aventura" }
              );

               context.SaveChanges();
            }


            if (!context.Libros!.Any())
            {
                context.AddRange(
                new Libro { Titulo = "El quijote de la mancha", CreateDate = "06/06/2020", Imagen = "quijote.jpg", Autor = "Miguel de Cervantes" },
                new Libro { Titulo = "Harry Potter", CreateDate = "06/01/2021", Imagen = "harry.jpg", Autor = "Juan de la vega" }               
              );               
            }

            if (!context.LibroCategorias!.Any())
            {
                context.AddRange(
                new LibroCategoria { CategoriaId = 1, LibroId = 1 },              
                new LibroCategoria { CategoriaId = 1, LibroId = 2 }              
              );
                context.SaveChanges();
            }

        }
    }
}
