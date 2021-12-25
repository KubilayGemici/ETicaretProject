using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.WebUI.APIs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartItemApi _cartItemApi;
        private readonly IMapper _mapper;
        private readonly IProductApi _productApi;
        private readonly IOrderApi _orderApi;
        private readonly IOrderDetailApi _orderDetailApi;
        private readonly IUserApi _userApi;
        private readonly ICartApi _cartApi;



        public OrderController(
            ICartItemApi cartItemApi,
            IProductApi productApi,
            IUserApi userApi,
            ICartApi cartApi,
            IOrderApi orderApi,
            IOrderDetailApi orderDetailApi,
            IMapper mapper)
        {
            _mapper = mapper;
            _productApi = productApi;
            _cartItemApi = cartItemApi;
            _userApi = userApi;
            _orderApi = orderApi;
            _orderDetailApi = orderDetailApi;
            _cartApi = cartApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        //order 
        public async Task<IActionResult> InsertOrder()
        {
            string id = Request.Cookies["UserId"];
            Guid musteriId = new Guid(id);
            OrderRequest orderRequest = new OrderRequest();
            var user = await _userApi.Get(musteriId);
            orderRequest.CustomerName = user.Content.FirstName;
            orderRequest.CustomerSurName = user.Content.LastName;
            orderRequest.Address = user.Content.Adress;
            orderRequest.UserId = musteriId;
            var listResult1 = await _cartItemApi.GetActive();
            int i = 0;
            decimal total = 0;
            foreach (var item in listResult1.Content)
            {
                if (i < listResult1.Content.Count)
                {
                    total += item.Total;
                }
                i++;
            }
            orderRequest.TotalPrice = total;
            var order = await _orderApi.Post(orderRequest);




            //orderdetail

            var cart = await _cartApi.UseriddenCartidBul(musteriId);
            Guid cartid = new Guid();
            cartid = cart.Content.Id;
            var listResult = await _cartItemApi.GetActive();
            i = 0;
            OrderDetailRequest detailRequest1 = new OrderDetailRequest();
            CartItemRequest cartItemRequest = new CartItemRequest();
            ProductRequest productRequest = new ProductRequest();
            foreach (var item in listResult.Content)
            {
                if (i < listResult.Content.Count)
                {
                    if (item.Status != Common.Client.Enums.Status.Deleted)
                    {
                        if (cartid == item.CartId)
                        {
                            var product = await _productApi.Get(item.ProductId);
                            var product1 = await _productApi.Get(item.ProductId);


                            detailRequest1.OrderId = order.Content.Id;
                            detailRequest1.ProductId = item.ProductId;
                            detailRequest1.ProductName = product.Content.ProductName;
                            detailRequest1.Quantity = item.Quantity;
                            detailRequest1.TotalPrice = item.Total;
                            await _orderDetailApi.Post(detailRequest1);

                            //Sepet Ürün Temizleme
                            cartItemRequest.Id = item.Id;
                            cartItemRequest.ProductId = item.ProductId;
                            cartItemRequest.ProductName = item.ProductName;
                            cartItemRequest.Quantity = item.Quantity;
                            cartItemRequest.Amount = item.Amount;
                            cartItemRequest.CartId = item.CartId;
                            cartItemRequest.Status = Common.Client.Enums.Status.Deleted;
                            await _cartItemApi.Put(item.Id, cartItemRequest);

                            //Stok Sistemi
                            
                            productRequest.UserId = musteriId;
                            productRequest.Id = item.ProductId;
                            productRequest.Image = product1.Content.Image;
                            productRequest.QuantityPerUnit = "1";
                            productRequest.ProductName = product1.Content.ProductName;
                            productRequest.Price = product1.Content.Price;
                            productRequest.Status = Common.Client.Enums.Status.None;
                            decimal sayi = 1;
                            decimal sayi2 = Convert.ToDecimal(product1.Content.UnitsInStock);
                            productRequest.UnitsInStock = Convert.ToInt16(sayi2 - sayi);
                            productRequest.CategoryId = product1.Content.CategoryId;
                            productRequest.Description = product1.Content.Description;
                            await _productApi.Put(productRequest.Id, productRequest);
                        }
                    }
                    
                }
                i++;
            }
            return (RedirectToAction("Index", "Home", new { area = "" }));
        }

    }
}
