using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IProductApi _productApi;
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;

        public HomeController(
            ICategoryApi categoryApi,
            IProductApi productApi,
            IUserApi userApi,
            IMapper mapper)
        {
            _categoryApi = categoryApi;
            _productApi = productApi;
            _userApi = userApi;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var productResult = await _productApi.GetActive();
            if (productResult.IsSuccessStatusCode && productResult.Content != null)
                products = _mapper.Map<List<ProductViewModel>>(productResult.Content.OrderByDescending(x => x.ViewCount).Take(5).ToList());
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
    }
}
