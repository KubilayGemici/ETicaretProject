using AutoMapper;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Models.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryApi _categoryApi;
        private readonly IMapper _mapper;

        public CategoryViewComponent(
            ICategoryApi categoryApi,
            IMapper mapper)
        {
            _categoryApi = categoryApi;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {
            //Component , Shared aynı isimli klasoru arar.
            List<CategoryViewModel> list = new List<CategoryViewModel>();
            var categoryResult = _categoryApi.GetActive().Result;
            if (categoryResult.IsSuccessStatusCode && categoryResult.Content.Any())
                list = _mapper.Map<List<CategoryViewModel>>(categoryResult.Content);
            return View(list);
        }
    }
}
