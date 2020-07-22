using BilgeAdamBitirmeProjesi.Common.Client.Models;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Content-Type: application/json")]
    public interface IAccountApi
    {
        //UserRequest parametresi alınacak ve geriye ApiResponse<WebApiResponse<UserResponse döndürülecek.
        [Get("/account/login")]
        Task<ApiResponse<WebApiResponse<UserResponse>>> Login([Query] UserRequest request);
    }
}
