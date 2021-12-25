using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.Model.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface ICartApi
    {
        [Get("/cart")]
        Task<ApiResponse<List<CartResponse>>> List();

        [Get("/cart/{id}")]
        Task<ApiResponse<CartResponse>> Get(Guid id);

        [Put("/cart/{id}")]
        Task<ApiResponse<CartResponse>> Put(Guid id, CartRequest request);

        [Post("/cart")]
        Task<ApiResponse<CartResponse>> Post(CartRequest request);

        [Delete("/cart/{id}")]
        Task<ApiResponse<CartResponse>> Delete(Guid id);

        [Get("/cart/activate/{id}")]
        Task<ApiResponse<CartResponse>> Activate(Guid id);

        [Get("/cart/getactive")]
        Task<ApiResponse<List<CartResponse>>> GetActive();

        [Get("/cart/cartiddenbul")]
        Task<ApiResponse<CartResponse>> UseriddenCartidBul(Guid userid);



    }
}
