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
using BilgeAdamBitirmeProjesi.WebUI.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;
        private readonly IUserApi _userApi;


        public AccountController(
            IAccountApi accountApi,
            IMapper mapper,
            IUserApi userApi)
        {
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
                        new Claim("Image",user.ImageUrl)
                    };

                    //Giriş işlemlerini tamamlayıp yönetici sayfasına yönlendireceğim.
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    HttpContext.Response.Cookies.Append("BilgeAdamAccessToken", user.AccessToken.AccessToken);
                    if (user.Title != "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            return View(request);
        }



        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string name)
        {
            var result = await _accountApi.CheckUser(email);

            if (result.Value == true)
            {
                ViewBag.uyari = "Böyle bir kullanıcı var";
            }
            else
            {

                var addUser = await _accountApi.AddUser(email, password, name);
            }



            return View("Login");

        }


        //Kullanıcı Çıkış İşlemleri
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
