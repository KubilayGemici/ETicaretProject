using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IOrderDetailApi
    {
        [Get("/orderdetail")]
        Task<ApiResponse<List<OrderDetailResponse>>> List();

        [Get("/orderdetail/{id}")]
        Task<ApiResponse<OrderDetailResponse>> Get(Guid id);

        [Put("/orderdetail/{id}")]
        Task<ApiResponse<OrderDetailResponse>> Put(Guid id, OrderDetailRequest request);

        [Post("/orderdetail")]
        Task<ApiResponse<OrderDetailResponse>> Post(OrderDetailRequest request);

        [Delete("/orderdetail/{id}")]
        Task<ApiResponse<OrderDetailResponse>> Delete(Guid id);

        [Get("/orderdetail/activate/{id}")]
        Task<ApiResponse<OrderDetailResponse>> Activate(Guid id);

        [Get("/orderdetail/getactive")]
        Task<ApiResponse<List<OrderDetailResponse>>> GetActive();
    }
}
