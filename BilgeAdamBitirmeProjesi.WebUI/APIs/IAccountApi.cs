using BilgeAdamBitirmeProjesi.Common.Client.Models;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using Microsoft.AspNetCore.Mvc;
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

        [Get("/account/check_user")]
        Task<ActionResult<bool>> CheckUser(string email);

        [Post("/account/add_user")]
        Task<ActionResult<bool>> AddUser(string email, string password,string name);

    }
}
