using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderDetailViewModels;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class OrderDetailController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserApi _us;
        private readonly IMapper _mp;
        private readonly IOrderDetailApi _od;

        public OrderDetailController(
            IWebHostEnvironment env,
            IUserApi us,
            IOrderDetailApi od,
            IMapper mp)
        {
            _env = env;
            _us = us;
            _od = od;
            _mp = mp;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OrderDetailViewModel> model = new List<OrderDetailViewModel>();
            var listModelResult = await _od.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                model = _mp.Map<List<OrderDetailViewModel>>(listModelResult.Content);
            return View(model);
        }
    }
}
