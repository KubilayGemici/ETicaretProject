using AutoMapper;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class OrderController
    {
        private readonly IWebHostEnvironment _env;
        private readonly IOrderApi _orderApi;
        private readonly IProductApi _productApi;
        private readonly ICategoryApi _categoryApi;
        private readonly IUserApi _userApi;
        private readonly IMapper _mapper;
        public OrderController(
            IWebHostEnvironment env,
            IProductApi productApi,
            ICategoryApi categoryApi,
            IUserApi userApi,
            IOrderApi orderApi,
            IMapper mapper)
        {
            _env = env;
            _orderApi = orderApi;
            _productApi = productApi;
            _categoryApi = categoryApi;
            _userApi = userApi;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return null;
        }
    }
}
