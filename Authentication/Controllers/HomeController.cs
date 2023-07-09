using Authentication.Models;
using Authentication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Authentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersDbContext usersDbContext;

        public IUserManger UserManger { get; }

        public HomeController(UsersDbContext usersDbContext, IUserManger userManger)
        {
            this.usersDbContext = usersDbContext;
            UserManger = userManger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                usersDbContext.Users.Add(
                    new User()
                    {
                        Login = registrationViewModel.Login,
                        PasswordHash = SHA256Encryptor.Encrypt(registrationViewModel.Password),
                        IsAdmin = registrationViewModel.IsAdmin
                    });

                await usersDbContext.SaveChangesAsync();

                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (UserManger.Login(loginViewModel.Login, loginViewModel.Password, loginViewModel.IsAdmin))
                    return RedirectToAction("Index", "Home");
            }


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}