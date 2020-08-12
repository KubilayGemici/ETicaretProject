using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IOrderDetail
    {
        [Get("/orderdetail")]
        Task<ApiResponse<List<OrderResponse>>> List();

        [Get("/orderdetail/{id}")]
        Task<ApiResponse<OrderResponse>> Get(Guid id);

        [Put("/orderdetail/{id}")]
        Task<ApiResponse<OrderResponse>> Put(Guid id, OrderRequest request);

        [Post("/orderdetail")]
        Task<ApiResponse<OrderResponse>> Post(OrderRequest request);

        [Delete("/orderdetail/{id}")]
        Task<ApiResponse<OrderResponse>> Delete(Guid id);

        [Get("/orderdetail/activate/{id}")]
        Task<ApiResponse<OrderResponse>> Activate(Guid id);

        [Get("/orderdetail/getactive")]
        Task<ApiResponse<List<OrderResponse>>> GetActive();
    }
}
