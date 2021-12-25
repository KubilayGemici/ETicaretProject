using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.CartItemViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.CartViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class CartController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly ICartApi _cartApi;
        private readonly ICartItemApi _cartItemApi;
        private readonly IMapper _mapper;
        private readonly IProductApi _productApi;

        public CartController(
            IWebHostEnvironment env,
            ICartApi cartApi,
            ICartItemApi cartItemApi,
            IProductApi productApi,
            IMapper mapper)
        {
            _env = env;
            _cartApi = cartApi;
            _productApi = productApi;
            _cartItemApi = cartItemApi;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertCart1(string miktar)
        {

            //Müşterinin sepeti yoksa sepet oluşturulucak , sepet var ise aynı sepet üzerinden ilerleyecek.
            string gelenid = Request.Cookies["UserId"];
            int adet = Convert.ToInt32(miktar);
            if (string.IsNullOrEmpty(gelenid))
            {
                //Guid productId = new Guid(Request.Cookies["ProductId"]);
                //HttpContext.Response.Cookies.Append("UrunAdi", pres.Content.Name);
                //HttpContext.Response.Cookies.Append("UrunMiktari", "1");
                //HttpContext.Response.Cookies.Append("UrunFiyati", (pres.Content.Price * 1).ToString());
                //cookiede cartid eksik adam giriş yapınca cart aşşağıdailer tetiklenecek

                return (RedirectToAction("Index2", "CartItem", new { area = "cart" , gelenmiktar = miktar.ToString()}));

            }
            else
            {
                Guid id = new Guid(gelenid);
                Guid cartid = new Guid();
                CartRequest item = new CartRequest();
                var sorgu = await _cartApi.UseriddenCartidBul(id);
                if (sorgu.Content == null)//kart yoksa oluştur varsa else kısmıylaidyi al
                {
                    item.UserId = id;
                    item.Status = Common.Client.Enums.Status.Active;
                    if (ModelState.IsValid)
                    {
                        var insertResult = await _cartApi.Post(item);
                        if (insertResult.IsSuccessStatusCode || insertResult.Content != null)
                        {
                            cartid = item.Id;
                        }
                        else
                            TempData["Message"] = "Sepete ürün eklemede bir sorun oluştu..Lütfen tekrar deneyin..";
                    }
                    else
                    {
                        TempData["Message"] = "Sepete ürün eklemede bir sorun oluştu..Lütfen tekrar deneyin..";
                    }
                }
                else
                {
                    var cart = await _cartApi.UseriddenCartidBul(id);
                    cartid = cart.Content.Id;
                }

                //CartItem kısmına ürünlerin eklenmesi
                CartItemRequest cr = new CartItemRequest();


                Guid productId = new Guid(Request.Cookies["ProductId"]);
                var pres = await _productApi.Get(productId);
                var cartitemler = await _cartItemApi.GetActive();

                List<Guid> pridler = new List<Guid>();

                CartItemRequest guncelleme = new CartItemRequest();
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
                if (((pres.Content.UnitsInStock) - adet) > 0)
                {
                    if (!pridler.Contains(productId))
                    {

                        cr.ProductId = pres.Content.Id;
                        cr.ProductName = pres.Content.ProductName;
                        cr.Quantity = adet;
                        cr.Amount = pres.Content.Price * adet;
                        cr.CartId = cartid;
                        await _cartItemApi.Post(cr);
                        return (RedirectToAction("Index", "Home", new { area = "" }));
                    }
                    else
                    {
                        foreach (var item1 in cartitemler.Content)
                        {
                            if (item1.CartId == cartid)
                            {
                                if (item1.ProductId == productId)
                                {
                                    var gnc = await _cartItemApi.Get(item1.Id);

                                    guncelleme.Id = gnc.Content.Id;
                                    guncelleme.ProductId = gnc.Content.ProductId;
                                    guncelleme.CartId = gnc.Content.CartId;
                                    guncelleme.ProductName = gnc.Content.ProductName;
                                    guncelleme.Quantity = gnc.Content.Quantity + adet;
                                    guncelleme.Amount = pres.Content.Price * guncelleme.Quantity;
                                    await _cartItemApi.Put(guncelleme.Id, guncelleme);
                                    return (RedirectToAction("Index", "Home", new { area = "" }));
                                }
                            }
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "" + pres.Content.ProductName + " isimli ürünü stokdakinden daha fazla eklemeye çalıştınız !!!";
                    return (RedirectToAction("Index", "CartItem", new { area = "" }));
                }
                return (RedirectToAction("Index", "Home", new { area = "" }));
            }
        }
    }
}