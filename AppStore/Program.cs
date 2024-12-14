using AppStore.Models.Domain;
using AppStore.Models.Domain.Context;
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(
    opt =>
    {
        opt.LogTo(Console.WriteLine, new[]{
            DbLoggerCategory.Database.Command.Name},
        LogLevel.Information).EnableSensitiveDataLogging();

        opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase"));
    }
  );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

using (var ambiente = app.Services.CreateScope())
{
    var service = ambiente.ServiceProvider;

    try {
        var context = service.GetRequiredService<DatabaseContext>();
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.MigrateAsync();
        await LoadDatabase.InsertarData(context, userManager, roleManager);
    }
    catch (Exception e) { 
        var logging = service.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "Ocurrio un error en la insercion de datos");
    }
}


app.Run();