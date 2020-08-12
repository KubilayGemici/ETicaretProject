using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface ICartItemApi
    {
        [Get("/cartıtem")]
        Task<ApiResponse<List<CartItemResponse>>> List();

        [Get("/cartıtem/{id}")]
        Task<ApiResponse<CartItemResponse>> Get(Guid id);

        [Put("/cartıtem/{id}")]
        Task<ApiResponse<CartItemResponse>> Put(Guid id, CartItemRequest request);

        [Post("/cartıtem")]
        Task<ApiResponse<CartItemResponse>> Post(CartItemRequest request);

        [Delete("/cartıtem/{id}")]
        Task<ApiResponse<CartItemResponse>> Delete(Guid id);

        [Get("/cartıtem/activate/{id}")]
        Task<ApiResponse<CartItemResponse>> Activate(Guid id);

        [Get("/cartıtem/getactive")]
        Task<ApiResponse<List<CartItemResponse>>> GetActive();
    }
}
