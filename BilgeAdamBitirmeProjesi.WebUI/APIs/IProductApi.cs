using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IProductApi
    {
        [Get("/product")]
        Task<ApiResponse<List<ProductResponse>>> List();

        [Get("/product/{id}")]
        Task<ApiResponse<ProductResponse>> Get(Guid id);

        [Put("/product/{id}")]
        Task<ApiResponse<ProductResponse>> Put(Guid id, ProductRequest request);

        [Post("/product")]
        Task<ApiResponse<ProductResponse>> Post(ProductRequest request);

        [Delete("/product/{id}")]
        Task<ApiResponse<ProductResponse>> Delete(Guid id);

        [Get("/product/activate/{id}")]
        Task<ApiResponse<ProductResponse>> Activate(Guid id);

        [Get("/product/getactive")]
        Task<ApiResponse<List<ProductResponse>>> GetActive();

        [Get("/product/GetByCategoryId/{categoryId}")]
        Task<ApiResponse<List<ProductResponse>>> GetByCategoryId(Guid categoryId);
    }
}
