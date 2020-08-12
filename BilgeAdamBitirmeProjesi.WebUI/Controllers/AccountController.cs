using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.UserViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class AccountController : Controller
    {


        private readonly IWebHostEnvironment _env;
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;
        private readonly IUserApi _userApi;


        public AccountController(
            IWebHostEnvironment webHost,
            IAccountApi accountApi,
            IMapper mapper,
            IUserApi userApi
            )
        {
            _env = webHost;
            _accountApi = accountApi;
            _mapper = mapper;
            _userApi = userApi;

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                //Geleni userrequest mapliyoruz.
                var loginResult = await _accountApi.Login(_mapper.Map<UserRequest>(request));
                if (loginResult.IsSuccessStatusCode && loginResult.Content.IsSuccess)
                {
                    UserResponse user = loginResult.Content.ResultData;
                    var claims = new List<Claim>()
                    {
                        new Claim("Id",user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.FirstName),
                        new Claim(ClaimTypes.Surname,user.LastName),
                        new Claim(ClaimTypes.Email,user.Email),
                    };

                    //Giriş işlemlerini tamamlayıp yönetici sayfasına veya Anasayfaya yönlendireceğim.
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    HttpContext.Response.Cookies.Append("BilgeAdamAccessToken", user.AccessToken.AccessToken);
                    HttpContext.Response.Cookies.Append("UserId", user.Id.ToString());
                    HttpContext.Response.Cookies.Append("isim", user.FirstName.ToString());
                    HttpContext.Response.Cookies.Append("soyisim", user.LastName.ToString());
                    HttpContext.Response.Cookies.Append("adres", user.Adress.ToString());
                    HttpContext.Response.Cookies.Append("telefon", user.Number.ToString());



                    if (user.Title != "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { name = "Müşteri" });
                    }
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            return View(request);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.FirstName, Email = model.Email };
                model.Status = Common.Client.Enums.Status.Active;
                model.Title = "Müşteri";
                var insertResult = await _userApi.Post(_mapper.Map<UserRequest>(model));

                if (insertResult.IsSuccessStatusCode || insertResult.Content != null)
                    return RedirectToAction("Index");
            }
            return View(model);
        }


        //Kullanıcı Çıkış İşlemleri
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
