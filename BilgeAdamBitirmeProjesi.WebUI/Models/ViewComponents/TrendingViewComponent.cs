using AutoMapper;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.ViewComponents
{
    public class TrendingViewComponent : ViewComponent
    {
        private readonly IProductApi _productApi;
        private readonly IMapper _mapper;
        public TrendingViewComponent(IProductApi productApi,IMapper mapper)
        {
            _productApi = productApi;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var productResult = _productApi.GetActive().Result;
            if (productResult.IsSuccessStatusCode && productResult.Content != null)
                products = _mapper.Map<List<ProductViewModel>>(productResult.Content.OrderByDescending(x => x.ViewCount).Take(3).ToList());
            else
                products = new List<ProductViewModel>();
            return View(products);
        }
    }
}
