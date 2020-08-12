using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.Core.Entity;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.Cart;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CartItemViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IProductApi _productApi;
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;
        private readonly ICartApi _cartApi;
        private readonly ICartItemApi _cartItemApi;

        public HomeController(
            ICategoryApi categoryApi,
            IProductApi productApi,
            IUserApi userApi,
            ICartApi cartApi,
            ICartItemApi cartItemApi,
            IMapper mapper)
        {
            _categoryApi = categoryApi;
            _productApi = productApi;
            _userApi = userApi;
            _cartApi = cartApi;
            _cartItemApi = cartItemApi;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int id, string name)
        {
            ViewBag.userLogin = name;

            List<ProductViewModel> products = new List<ProductViewModel>();
            var productResult = await _productApi.GetActive();
            if (productResult.IsSuccessStatusCode && productResult.Content != null)
                products = _mapper.Map<List<ProductViewModel>>(productResult.Content.OrderByDescending(x => x.Id).Take(5).ToList());
            else
                products = new List<ProductViewModel>();

            ViewBag.basket = id;

            return View(products);
        }

        public async Task<IActionResult> Products(Guid id)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            List<ProductViewModel> products1 = new List<ProductViewModel>();

            var productResult = await _productApi.GetByCategoryId(id);
            if (productResult.IsSuccessStatusCode && productResult.Content != null)
                products = _mapper.Map<List<ProductViewModel>>(productResult.Content);
            else
                products = new List<ProductViewModel>();
            foreach (ProductViewModel item in products.Where(x => x.Status == Common.Client.Enums.Status.Active))
            {
                products1.Add(item);
            }

            return View(products1);
        }

        public async Task<IActionResult> Product(Guid id)
        {
            var getProductResult = await _productApi.Get(id);
            if (getProductResult.IsSuccessStatusCode && getProductResult.Content != null)
            {
                ProductViewModel x = _mapper.Map<ProductViewModel>(getProductResult.Content);
                x.ViewCount++;
                var updateResult = await _productApi.Put(x.Id, _mapper.Map<ProductRequest>(x));
                if (updateResult.IsSuccessStatusCode && updateResult.Content != null)
                {
                    ProductViewModel updatedProduct = _mapper.Map<ProductViewModel>(updateResult.Content);
                    UserViewModel userViewModel = _mapper.Map<UserViewModel>((await _userApi.Get(x.UserId)).Content);
                    return View(Tuple.Create<ProductViewModel, UserViewModel>(updatedProduct, userViewModel));
                }
            }
            return View();
        }

        public async Task<IActionResult> AddCart(Guid id)
        {
            var addBasket = 0;

            ViewBag.basket = addBasket + 1;

            //var getProductResult = await _productApi.Get(id);
            //if (getProductResult.IsSuccessStatusCode && getProductResult.Content != null)
            //{
            //    ProductViewModel x = _mapper.Map<ProductViewModel>(getProductResult.Content);
            //    var updateResult = await _productApi.Put(x.Id, _mapper.Map<ProductRequest>(x));
            //    if (updateResult.IsSuccessStatusCode && updateResult.Content != null)
            //    {
            //        ProductViewModel updatedProduct = _mapper.Map<ProductViewModel>(updateResult.Content);
            //        UserViewModel userViewModel = _mapper.Map<UserViewModel>((await _userApi.Get(x.UserId)).Content);
            //        return View(Tuple.Create<ProductViewModel, UserViewModel>(updatedProduct, userViewModel));
            //    }
            //}
            return View();
        }



        public ActionResult addcartcustom(int id)
        {
            var addbasket = 0;

            addbasket = id + 1;

            return RedirectToAction("Index", new { id = addbasket });

        }

        //public async Task<ActionResult<CartResponse>> AddCartCustom()
        //{
        //    var value = Request.Cookies["UserId"];
        //    Guid result = new Guid(value);


        //    var cartAdded = new Cart();
        //    cartAdded.Id = Guid.NewGuid();
        //    cartAdded.CreatedDate = DateTime.Now;
        //    cartAdded.ModifiedDate = DateTime.Now;
        //    cartAdded.Status = Core.Entity.Enums.Status.Active;
        //    if(result != null)
        //    {
        //        cartAdded.UserId = result;

        //    }

        //    Cart entity = _mapper.Map<Cart>(cartAdded);
        //    var insertResult = await _cartApi.Post(entity);
        //    //if (insertResult != null)
        //    //    return CreatedAtAction("GetCart", new { id = insertResult.Id }, _mapper.Map<CartResponse>(insertResult));
        //    //else
        //    return RedirectToAction("Index");
        //}

        public IActionResult EmptyCart()
        {
            return View();
        }


        public IActionResult UserInformation()
        {
            ViewBag.name = Request.Cookies["isim"];
            ViewBag.surename = Request.Cookies["soyisim"];
            ViewBag.adress = Request.Cookies["adres"];
            ViewBag.tel = Request.Cookies["telefon"];


            return View();
        }
    }
}
