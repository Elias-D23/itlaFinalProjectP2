using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using VoteLine.Web.Models.ViewModels;
using System.Formats.Asn1;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using VoteLine.Domain.Entities;
using VoteLine.Domain;
using VoteLine.Persistence;

namespace VoteLine.Web.Controllers
{
    public class AccessController : Controller
    {

        private readonly VoteLineDbContext _Dbcontext;

        public AccessController(VoteLineDbContext context)
        {
            _Dbcontext = context;
        }

        // Método para validar si el usuario está autenticado
        //public User GetAuthenticatedUser(HttpContext context)
        //{
        //    if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
        //        return null;

        //    var userEmail = context.User.Identity.Name;
        //    return _Dbcontext.Users.FirstOrDefault(u => u.Email == userEmail);
        //}

        public User GetAuthenticatedUser(HttpContext context)
        {
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
                return null;

            // Verificar si el Claim existe
            var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
                return null;

            return _Dbcontext.Users.FirstOrDefault(u => u.Email == userEmail);
        }


        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
            
            return View();
        }

        //Register . . .
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserVM model)
        {

            if (model.Password != model.ConfirmPassword)
            {
                ViewData["Message"] = "The password is incorrect";
                return View();
            }

            User user = new User()
            {
                FullName = model.FullName,
                Password = model.Password,
                Email = model.Email,
                DNI = model.DNI,
                IsAdmin = model.IsAdmin,
            };

            await _Dbcontext.Users.AddAsync(user);
            await _Dbcontext.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }

        //Login . . .
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

            return View();
        }
    
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
    
            User user = await _Dbcontext.Users.Where(u => 
            u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();

            if (user == null) {

                ViewData["Message"] = "This user is not registered.";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Index", "Home");
            //return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "Sesión cerrada correctamente.";
            return RedirectToAction("Login", "Access");
        }

        //public IActionResult Logout()
        //{
        //    // Eliminar la cookie del usuario
        //    Response.Cookies.Delete("UserEmail");
        //    TempData["SuccessMessage"] = "Sesión cerrada correctamente.";
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<User> listUsers = await _Dbcontext.Users.ToListAsync();
            return View(listUsers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            User user = await _Dbcontext.Users.FirstAsync(e => e.UserId == id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            _Dbcontext.Users.Update(user);
            await _Dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await _Dbcontext.Users.FirstAsync(e => e.UserId == id);

            _Dbcontext.Users.Remove(user);
            await _Dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
