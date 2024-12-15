# 📚 LibreriaStore

## Descripción

**LibreriaStore** es un proyecto web construido utilizando **ASP.NET Core**, que permite la gestión de una librería en línea. El proyecto implementa la arquitectura **MVC** y utiliza el **patrón Repository** para separar la lógica de acceso a datos del resto de la aplicación. Además, se integra con **Entity Framework Core** para la gestión de la base de datos y **SQLite** como proveedor de almacenamiento.

---

## 🚀 Funcionalidades principales

- **Gestión de libros**:
  - Crear, editar y eliminar libros.
  - Asignar múltiples categorías a un libro.
  - Subir imágenes para los libros.
- **Gestión de categorías**:
  - Crear y listar categorías de libros.
- **Autenticación y autorización**:
  - Registro, inicio de sesión y cierre de sesión.
  - Gestión de roles y permisos (administrador y usuario).
- **Sistema de archivos**:
  - Subida y eliminación de imágenes asociadas a libros.

---

## 🛠️ Tecnologías utilizadas

- **Backend**: ASP.NET Core
- **Arquitectura**: MVC (Model-View-Controller)
- **ORM**: Entity Framework Core
- **Base de datos**: SQLite
- **Autenticación**: Identity Framework
- **Lenguajes**: C#, Razor Pages (HTML + C#)
- **Control de versiones**: Git y GitHub

---

## 📂 Estructura del proyecto

### **Capas principales**
- **`Models`**:
  - Define las entidades como `Libro`, `Categoria`, y `ApplicationUser`.
- **`Repositories`**:
  - **Abstract**: Define las interfaces como `ILibroService`, `ICategoriaService`, etc.
  - **Implementation**: Contiene las implementaciones concretas del patrón Repository, como `LibroService` y `FileService`.
- **`Controllers`**:
  - Controla la lógica de interacción entre el usuario y el sistema.
- **`Views`**:
  - Contiene las vistas en Razor Pages para presentar la interfaz al usuario.

---

## 📦 Instalación y configuración

### **Requisitos previos**
- Tener instalado .NET 6 o superior.
- Visual Studio 2022 o cualquier IDE compatible con .NET Core.
- SQLite para base de datos.

### **Pasos**
1. Clona el repositorio:
   ```bash
   git clone https://github.com/Miguel-Calderon-Gutierrez/AppStore.git
   ```
2. Abre la solución `AppStoreSolution.sln` en tu IDE.
3. Restaura las dependencias:
   ```bash
   dotnet restore
   ```
4. Configura la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "SqliteDatabase": "Data Source=LocalDatabase.db"
   }
   ```
5. Ejecuta las migraciones para crear la base de datos:
   ```bash
   dotnet ef database update
   ```
6. Ejecuta el proyecto:
   ```bash
   dotnet run
   ```

---

¡Gracias por revisar **LibreriaStore**! 🎉