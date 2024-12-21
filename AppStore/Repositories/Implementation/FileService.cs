using AppStore.Repositories.Abstract;
using System.Linq.Expressions;

namespace AppStore.Repositories.Implementation
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {

                var wwwPath = this._environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads");

                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var ext = Path.GetExtension(imageFile.FileName);
                var allowebExtensions = new string[] { ".jpg", ".png", ".jpeg" };

                if (!allowebExtensions.Contains(ext))
                {
                    var message = $"Solo están permitidas las extensiones {allowebExtensions}";
                    return Tuple.Create(0, message);
                }

                var uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;

                var fileWithPath = Path.Combine(path, newFileName);

                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();

                return Tuple.Create(1, newFileName);

            }
            catch (Exception e)
            {
                return Tuple.Create(0, "Error al guardar la imagen");
            }
        }

        public bool DeleteImage(string imagenName)
        {
            try
            {
                var wwwPath = this._environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\", imagenName);

                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
