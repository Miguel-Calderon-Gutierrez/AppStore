using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, 
                                         SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel login)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(login.Username!);

            if (user == null) {
                status.StatusCode = 0;
                status.Message = "El username es invalido";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, login.Password!)) {
                status.StatusCode = 0;
                status.Message = "El Password es invalido";
                return status;
            }

            var resultado = await _signInManager.PasswordSignInAsync(user, login.Password!, true, false);

            if (resultado == null)
            {
                status.StatusCode = 0;
                status.Message = "Las credenciales son incorrectas";
            }
            else {
                status.StatusCode = 1;
                status.Message = "Login exitoso";
            }

            return status;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
