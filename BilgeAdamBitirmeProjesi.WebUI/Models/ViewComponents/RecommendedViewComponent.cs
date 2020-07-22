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
    public class RecommendedViewComponent : ViewComponent
    {
        private readonly IProductApi _productApi;
        private readonly IMapper _mapper;
        public RecommendedViewComponent(IProductApi productApi,
            IMapper mapper)
        {
            _productApi = productApi;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            List<ProductViewModel> recommended = new List<ProductViewModel>();
            var productResult = _productApi.GetActive().Result;
            if (productResult.IsSuccessStatusCode && productResult.Content != null)
                recommended = _mapper.Map<List<ProductViewModel>>(productResult.Content.OrderBy(x => Guid.NewGuid()).Take(3).ToList());
            else
                recommended = new List<ProductViewModel>();
            return View(recommended);
        }
    }
}
