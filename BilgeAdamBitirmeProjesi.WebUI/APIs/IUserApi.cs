using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IUserApi
    {
        [Get("/user")]
        Task<ApiResponse<List<UserResponse>>> List();

        [Get("/user/{id}")]
        Task<ApiResponse<UserResponse>> Get(Guid id);

        [Put("/user/{id}")]
        Task<ApiResponse<UserResponse>> Put(Guid id, UserRequest request);

        [Post("/user")]
        Task<ApiResponse<UserResponse>> Post(UserRequest request);

        [Delete("/user/{id}")]
        Task<ApiResponse<UserResponse>> Delete(Guid id);

        [Get("/user/activate/{id}")]
        Task<ApiResponse<UserResponse>> Activate(Guid id);
    }
}
