using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderDetailViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.UserViewModels;
using BilgeAdamBitirmeProjesi.Common.DTOs.Order;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICategoryApi _categoryApi;
        private readonly IProductApi _productApi;
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;
        private readonly ICartApi _cartApi;
        private readonly IOrderApi _orderApi;
        private readonly ICartItemApi _cartItemApi;
        private readonly IOrderDetailApi _orderDetailApi;

        public HomeController(
            ICategoryApi categoryApi,
            IProductApi productApi,
            IOrderApi orderApi,
            IUserApi userApi,
            ICartApi cartApi,
            ICartItemApi cartItemApi,
            IOrderDetailApi orderDetailApi,
            IMapper mapper)
        {
            _categoryApi = categoryApi;
            _productApi = productApi;
            _userApi = userApi;
            _cartApi = cartApi;
            _cartItemApi = cartItemApi;
            _orderApi = orderApi;
            _orderDetailApi = orderDetailApi;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var productResult = await _productApi.GetActive();
            if (productResult.IsSuccessStatusCode && productResult.Content != null)
                products = _mapper.Map<List<ProductViewModel>>(productResult.Content.OrderByDescending(x => x.Id).Take(5).ToList());
            else
                products = new List<ProductViewModel>();

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

                var updateResult = await _productApi.Get(id);
                ProductViewModel updatedProduct = _mapper.Map<ProductViewModel>(updateResult.Content);
                UserViewModel userViewModel = _mapper.Map<UserViewModel>((await _userApi.Get(x.UserId)).Content);
                HttpContext.Response.Cookies.Append("ProductId", id.ToString());
                return View(Tuple.Create<ProductViewModel, UserViewModel>(updatedProduct, userViewModel));
            }
            return View();
        }


        //public IActionResult UserInformation()
        //{
        //    //ViewBag.name = Request.Cookies["isim"];
        //    //ViewBag.surname = Request.Cookies["soyisim"];
        //    //ViewBag.adress = Request.Cookies["adres"];
        //    //ViewBag.tel = Request.Cookies["telefon"];

        //    //return View();
        //}


        public async Task<IActionResult> CostumerOrder()
        {
          
            Guid userID = new Guid(Request.Cookies["UserId"]);
            List<OrderResponse> model = new List<OrderResponse>();
            var listModelResult = await _orderApi.List();
            foreach (var item in listModelResult.Content)
            {
                if (item.UserId==userID)
                {
                    model.Add(item);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> CostumerOrder2(Guid id)
        {

            var orderdetail = await _orderDetailApi.List();
            List<OrderDetailResponse> userOrderDetail = new List<OrderDetailResponse>();
            foreach (OrderDetailResponse item in orderdetail.Content)
            {
                if (item.OrderId == id)
                {
                    var product = await _productApi.Get(item.ProductId);
                    item.TotalPrice = item.Quantity * product.Content.Price;
                    userOrderDetail.Add(item);
                }
            }
            return View(userOrderDetail);
        }
    }
}
