using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IOrderApi
    {
        [Get("/order")]
        Task<ApiResponse<List<OrderResponse>>> List();

        [Get("/order/{id}")]
        Task<ApiResponse<OrderResponse>> Get(Guid id);

        [Put("/order/{id}")]
        Task<ApiResponse<OrderResponse>> Put(Guid id, OrderRequest request);

        [Post("/order")]
        Task<ApiResponse<OrderResponse>> Post(OrderRequest request);

        [Delete("/order/{id}")]
        Task<ApiResponse<OrderResponse>> Delete(Guid id);

        [Get("/order/activate/{id}")]
        Task<ApiResponse<OrderResponse>> Activate(Guid id);

        [Get("/order/getactive")]
        Task<ApiResponse<List<OrderResponse>>> GetActive();
    }
}
