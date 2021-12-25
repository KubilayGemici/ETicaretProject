using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Models.OrderViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class OrderController : Controller
    {

        private readonly IOrderApi _orderApi;
        private readonly IProductApi _productApi;
        private readonly IOrderDetailApi _orderDetailApi;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderApi orderApi,
            IProductApi productApi,
            IOrderDetailApi orderDetailApi,
            IMapper mapper
            )
        {
            _orderApi = orderApi;
            _productApi = productApi;
            _orderDetailApi = orderDetailApi;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OrderViewModel> model = new List<OrderViewModel>();
            var listModelResult = await _orderApi.List();
            if (listModelResult.IsSuccessStatusCode && listModelResult.Content.Any())
                model = _mapper.Map<List<OrderViewModel>>(listModelResult.Content);
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedResult = await _orderApi.Delete(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Gönderildi(Guid id)
        {
            var activateResult = await _orderApi.Activate(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index2(Guid id)
        {
            var orderdetails = await _orderDetailApi.List();
            List<OrderDetailResponse> gonderilecekler = new List<OrderDetailResponse>();
            foreach (OrderDetailResponse item in orderdetails.Content)
            {
                if (item.OrderId == id)
                {
                    var product = await _productApi.Get(item.ProductId);
                    item.TotalPrice = item.Quantity * product.Content.Price;
                    gonderilecekler.Add(item);
                }
            }
            return View(gonderilecekler);
        }

    }
}
