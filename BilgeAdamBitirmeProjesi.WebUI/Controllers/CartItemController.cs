using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.CartItemViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Models.ProductViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemApi _cartItemApi;
        private readonly IMapper _mapper;
        private readonly IProductApi _productApi;
        private readonly ICartApi _cartApi;

        public CartItemController(
            ICartItemApi cartItemApi,
             IProductApi productApi,
             ICartApi cartApi,
            IMapper mapper)
        {
            _mapper = mapper;
            _productApi = productApi;
            _cartItemApi = cartItemApi;
            _cartApi = cartApi;
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //burada member idden cartidbul oradanda cartitems listele

            List<CartItemResponse> list = new List<CartItemResponse>();
            List<Guid> productidler = new List<Guid>();
            List<Guid> silinecekidler = new List<Guid>();

            string id = Request.Cookies["UserId"];
            Guid musteriid = new Guid(id);
            var cart = await _cartApi.UseriddenCartidBul(musteriid);
            List<CartItemResponse> gonderilecekler = new List<CartItemResponse>();
            Guid cartid = new Guid();
            if (cart.Content != null)
            {
                cartid = cart.Content.Id;

                var listResult = await _cartItemApi.GetActive();
                int i = 0;
                decimal total = 0;

                foreach (var item in listResult.Content)
                {
                    if (i < listResult.Content.Count)
                    {
                        if (cartid == item.CartId)
                        {
                            gonderilecekler.Add(item);
                        }
                    }
                }
                foreach (var item in gonderilecekler)
                {
                    if (i < gonderilecekler.Count)
                    {
                        var product = _productApi.Get(item.ProductId);
                        gonderilecekler[i].Image = product.Result.Content.Image;
                        if (productidler.Contains(item.ProductId))
                        {
                            silinecekidler.Add(item.Id);
                            Guid silinencartid = gonderilecekler.Where(x => x.ProductId == gonderilecekler[i].ProductId).Select(x => x.Id).FirstOrDefault();
                            int a = 0;
                            foreach (var item1 in gonderilecekler)
                            {
                                if (item1.Id == silinencartid)
                                {
                                    gonderilecekler[a].Quantity++;
                                }
                                a++;
                            }
                        }
                        productidler.Add(item.ProductId);
                    }
                    i++;
                }
                foreach (var item in silinecekidler)
                {
                    gonderilecekler.Remove(gonderilecekler.Where(x => x.Id == item).LastOrDefault());
                }
                int o = 0;
                foreach (var item in gonderilecekler)
                {
                    gonderilecekler[o].Total = item.Total / item.Quantity;
                    total += item.Total * item.Quantity;
                    gonderilecekler[o].Total = total;
                    o++;

                }
                //if (listResult.IsSuccessStatusCode && listResult.Content.Any())
                //    list = _mapper.Map<List<CartItemResponse>>(listResult.Content);


            }
            //else
            //{
            //    CartItemResponse bosdeger = new CartItemResponse();
            //    bosdeger.ProductName = "Ürün bulunmamaktadır.";
            //    bosdeger.Quantity = 0;
            //    bosdeger.Total = 0;
            //    bosdeger.TotalPrice = 0;
            //    gonderilecekler.Add(bosdeger);
            //}

            return View(gonderilecekler);

        }
        [HttpGet]
        public async Task<IActionResult> Index2(string area, string gelenmiktar)
        {
            List<ProductVM> gonderilecekler = new List<ProductVM>();
            //BURAYA GELEN MİKTAR KADAR FOREACH ATARSAN MİKTAR KADAR COKKİE EKLENİR.
            if (!string.IsNullOrEmpty(area))
            {
                if (string.IsNullOrEmpty(gelenmiktar) || gelenmiktar == "0")
                {
                    gelenmiktar = "1";
                }
                Guid id = new Guid(Request.Cookies["ProductId"]); //buradan ürünü buluyorum.Bir product ekranına girdiğinde cookiye ekleniyor.
                int adet = Convert.ToInt32(gelenmiktar);
                var urun = await _productApi.Get(id);
                ProductVM vm = new ProductVM
                {
                    Id = urun.Content.Id,
                    ProductName = urun.Content.ProductName,
                    UnitPrice = urun.Content.Price,
                    CategoryId = urun.Content.CategoryId,
                    UnitsInStock = urun.Content.UnitsInStock,
                    Quantity = adet,
                    Image = urun.Content.Image
                };
                //HttpContext.Session.
                decimal sepetfiyat = 0;
                int sepetmiktar = 0;
                if (HttpContext.Session.GetString("sepet") != null)
                {
                    List<ProductVM> session = JsonConvert.DeserializeObject<List<ProductVM>>(HttpContext.Session.GetString("sepet"));
                    int sayac = 0;
                    int silinecek = -1;
                    foreach (var item in session)
                    {
                        if (item.Id == vm.Id)
                        {
                            item.Quantity += vm.Quantity;
                            silinecek = sayac;
                        }


                        if (item.UnitsInStock < item.Quantity)
                        {
                            //burada hata alınması gerekiyor stok miktarında fazla ürün eklenmeye çalışıldı.
                            TempData["hata"] = "STOK MİKTARINDAN DAHA FAZLA ÜRÜN EKLEMEYE ÇALIŞTINIZ !!!";
                            return RedirectToAction("Product", "Home", new { id = id });
                        }
                        sayac++;
                    }
                    HttpContext.Session.Remove("sepet");
                    if (silinecek < 0)
                    {
                        session.Add(vm);
                    }
                    HttpContext.Session.SetString("sepet", JsonConvert.SerializeObject(session));
                    foreach (var item in session)
                    {
                        sepetfiyat += item.UnitPrice * item.Quantity;
                        sepetmiktar += item.Quantity;
                    }
                }
                else
                {
                    if (vm.Quantity > vm.UnitsInStock)
                    {
                        TempData["hata"] = "STOK MİKTARINDAN DAHA FAZLA ÜRÜN EKLEMEYE ÇALIŞTINIZ !!!";
                        return RedirectToAction("Product", "Home", new { id = id });
                    }
                    List<ProductVM> urunler = new List<ProductVM>();
                    sepetmiktar = vm.Quantity;
                    sepetfiyat = vm.UnitPrice * vm.Quantity;
                    urunler.Add(vm);
                    HttpContext.Session.SetString("sepet", JsonConvert.SerializeObject(urunler));
                }

                TempData["hata"] = "" + gelenmiktar + " ADET ÜRÜN SEPETİNİZE EKLENMİŞTİR.";


                HttpContext.Response.Cookies.Append("sepettekiurun", sepetmiktar.ToString());
                HttpContext.Response.Cookies.Append("sepettekifiyat", sepetfiyat.ToString());
                return RedirectToAction("Product", "Home", new { id = id });
            }
            if (HttpContext.Session.GetString("sepet") != null)
            {
                List<ProductVM> session = JsonConvert.DeserializeObject<List<ProductVM>>(HttpContext.Session.GetString("sepet"));
                gonderilecekler = session;
            }
            return View(gonderilecekler);

        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var cartitem = await _cartItemApi.Get(id);
            var product = await _productApi.Get(cartitem.Content.ProductId);
            if ((cartitem.Content.Quantity - 1) == 0)
            {
                await _cartItemApi.Delete(id);
            }
            else
            {
                CartItemRequest guncelleme = new CartItemRequest();
                guncelleme.Id = cartitem.Content.Id;
                guncelleme.ProductId = cartitem.Content.ProductId;
                guncelleme.CartId = cartitem.Content.CartId;
                guncelleme.ProductName = cartitem.Content.ProductName;
                guncelleme.Quantity = cartitem.Content.Quantity - 1;
                guncelleme.Amount = product.Content.Price * guncelleme.Quantity;
                await _cartItemApi.Put(guncelleme.Id, guncelleme);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete1(Guid id)
        {
            if (HttpContext.Session.GetString("sepet") != null)
            {
                List<ProductVM> session = JsonConvert.DeserializeObject<List<ProductVM>>(HttpContext.Session.GetString("sepet"));
                string urunid = null;
                foreach (var item in session)
                {
                    if (item.Id == id)
                    {
                        item.Quantity--;
                        if (item.Quantity <= 0)
                        {
                            urunid = item.Id.ToString();
                        }
                    }
                }
                if (urunid != null)
                {
                    Guid silinecekid = new Guid(urunid);
                    session.Remove(session.Where(x => x.Id == silinecekid).FirstOrDefault());
                }
                HttpContext.Session.Remove("sepet");
                HttpContext.Session.SetString("sepet", JsonConvert.SerializeObject(session));
                decimal sepetfiyat = 0;
                int sepetmiktar = 0;
                foreach (var item in session)
                {
                    sepetfiyat += item.Quantity * item.UnitPrice;
                    sepetmiktar += item.Quantity;
                }

                HttpContext.Response.Cookies.Append("sepettekiurun", sepetmiktar.ToString());
                HttpContext.Response.Cookies.Append("sepettekifiyat", sepetfiyat.ToString());
            }
            return RedirectToAction("Index2");
        }
    }
}

