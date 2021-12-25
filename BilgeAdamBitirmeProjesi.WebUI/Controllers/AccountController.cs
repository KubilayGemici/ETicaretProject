using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Models.AccountViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class AccountController : Controller
    {


        private readonly IWebHostEnvironment _env;
        private readonly IAccountApi _accountApi;
        private readonly IMapper _mapper;
        private readonly IUserApi _userApi;
        private readonly ICartApi _cartApi;
        private readonly IProductApi _productApi;
        private readonly ICartItemApi _cartItemApi;


        public AccountController(
            IWebHostEnvironment webHost,
            IAccountApi accountApi,
            IMapper mapper,
            IUserApi userApi, IProductApi productApi, ICartItemApi cartItemApi, ICartApi cartApi
            )
        {
            _env = webHost;
            _accountApi = accountApi;
            _mapper = mapper;
            _userApi = userApi;
            _productApi = productApi;
            _cartItemApi = cartItemApi;
            _cartApi = cartApi;

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
                    if (user.Email != "admin@admin.com" || user.Password != "123")
                    {



                        //CART KISIMLARI -------------------------------------------------

                        string gelenid = user.Id.ToString();
                        Guid id = new Guid(gelenid);
                        Guid cartid = new Guid();
                        CartRequest crq = new CartRequest();
                        var sorgu = await _cartApi.UseriddenCartidBul(id);
                        if (sorgu.Content == null)//kart yoksa oluştur varsa else kısmıylaidyi al
                        {
                            crq.UserId = id;
                            crq.Status = Common.Client.Enums.Status.Active;
                            if (ModelState.IsValid)
                            {
                                var insertResult = await _cartApi.Post(crq);
                                if (insertResult.IsSuccessStatusCode || insertResult.Content != null)
                                {
                                    cartid = crq.Id;
                                }
                                else
                                    TempData["Message"] = "Kayıt sırasında bir hata oluştu.Lütfen tüm alanları kontrol edip tekrar deneyiniz.";
                            }
                            else
                            {
                                TempData["Message"] = "İşlem başarısız oldu!....Lütfen tüm alanları kontrol edip tekrar deneyiniz.";
                            }
                        }
                        else
                        {
                            var cart = await _cartApi.UseriddenCartidBul(id);
                            cartid = cart.Content.Id;
                        }
                        //CART KISIMLARI BİTTİ -------------------------------------------------------------
                        if (HttpContext.Session.GetString("sepet") != null)
                        {
                            List<ProductResponse> gonderilecekler1 = new List<ProductResponse>();
                            List<ProductVM> urunler = JsonConvert.DeserializeObject<List<ProductVM>>(HttpContext.Session.GetString("sepet"));
                            ProductResponse response = new ProductResponse();
                            foreach (var item in urunler)
                            {
                                response.Id = item.Id;
                                response.ProductName = item.ProductName;
                                response.UnitsInStock =Convert.ToInt16(item.Quantity);
                                response.Price = item.UnitPrice;
                                response.Image = item.Image;
                                response.CreatedDate = DateTime.Now;
                                response.CategoryId = item.CategoryId;
                                gonderilecekler1.Add(response);
                            }


                            //CARTITEM KAYITLARI BAŞLIYOR ----------------------------------
                            CartItemRequest cr = new CartItemRequest();
                            var cartitemler = await _cartItemApi.GetActive();
                            List<Guid> pridler = new List<Guid>();
                            CartItemRequest guncelleme = new CartItemRequest();

                            foreach (var item in gonderilecekler1)
                            {
                                if (cartitemler.Content != null)
                                {
                                    if (cartitemler.Content.Count > 0)
                                    {
                                        foreach (var item1 in cartitemler.Content)
                                        {
                                            if (item1.CartId == cartid)
                                            {
                                                pridler.Add(item1.ProductId);
                                            }
                                        }
                                    }
                                }
                                if (!pridler.Contains(item.Id)) //CARTİTEMLARINDA YOKSA EKLEDİM
                                {
                                    cr.ProductId = item.Id;
                                    cr.ProductName = item.ProductName;
                                    cr.Quantity = item.UnitsInStock;
                                    cr.Amount = item.Price * item.UnitsInStock;
                                    cr.CartId = cartid;
                                    await _cartItemApi.Post(cr);
                                    pridler.Add(item.Id);
                                }


                                else  //CARTITEMLARININ İÇERİSİNDE VARSA MİKTAR ARTSIN
                                {
                                    foreach (var item1 in cartitemler.Content)
                                    {
                                        if (item1.CartId == cartid)
                                        {
                                            if (item1.ProductId == item.Id)
                                            {
                                                var gnc = await _cartItemApi.Get(item1.Id);

                                                guncelleme.Id = gnc.Content.Id;
                                                guncelleme.ProductId = gnc.Content.ProductId;
                                                guncelleme.CartId = gnc.Content.CartId;
                                                guncelleme.ProductName = gnc.Content.ProductName;
                                                guncelleme.Quantity =Convert.ToInt16(gnc.Content.Quantity) + item.UnitsInStock;
                                                guncelleme.Amount = item.Price * guncelleme.Quantity;
                                                await _cartItemApi.Put(guncelleme.Id, guncelleme);
                                            }
                                        }
                                    }
                                }
                            }
                            //CARTITEM KAYITLARI BİTTİ  ----------------------------------------
                            HttpContext.Session.Remove("sepet");
                        }
                        await HttpContext.SignInAsync(principal);


                        return (RedirectToAction("Index", "Home", new { area = "" }));
                    }

                    if (user.Title != "Admin")
                    {
                        await HttpContext.SignInAsync(principal);
                        return RedirectToAction("Index", "Home");
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
                model.Status = Common.Client.Enums.Status.Active;
                model.Title = "Müşteri";
                var insertResult = await _userApi.Post(_mapper.Map<UserRequest>(model));
                if (insertResult.IsSuccessStatusCode || insertResult.Content != null)
                    return RedirectToAction("Index", "Home", new { area = "" });
                else
                    TempData["Message"] = "Kayıt işleminde bir hata oluştu. Lütfen tüm alanları kontrol ediniz.";
            }
            else
            {
                TempData["Message"] = "İşlem Başarısız oldu. Lütfen tüm alanları kontrol ediniz.";
            }
            return View(model);
        }


        //Kullanıcı Çıkış İşlemleri
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            //COOKİE SİLME İSLEMLERİ
            string key = "UserId";
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append(key, value, options); 
            Response.Cookies.Append("sepettekiurun", value, options);
            Response.Cookies.Append("sepetmiktar", value, options);
            Response.Cookies.Append("UserId", value, options);
            ///////
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}