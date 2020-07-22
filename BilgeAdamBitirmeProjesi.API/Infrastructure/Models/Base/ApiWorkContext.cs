using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.Client.Services;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.API.Infrastructure.Models.Base
{
    public class ApiWorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public ApiWorkContext(
            IHttpContextAccessor httpContextAccessor, 
            IUserService userService,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }
        public UserResponse CurrentUser
        { 
            get 
            {
                var authResult = _httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme).Result;
                if (!authResult.Succeeded)
                    return null;

                var email = authResult.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                var userId = authResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                //Gelen ıd string geldiği için parse ettim.Asenkron olmadığı için Await yerine , Result verdim.
                //UserResponse user = _userService.GetById(Guid.Parse(userId)).Result;
                UserResponse user = _mapper.Map<UserResponse>(_userService.GetById(Guid.Parse(userId)).Result);
                return user;
            }
            set 
            {
                CurrentUser = value;
            }
        }
    }
}
