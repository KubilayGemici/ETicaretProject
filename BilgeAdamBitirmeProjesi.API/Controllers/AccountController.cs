using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.Client.Extensions;
using BilgeAdamBitirmeProjesi.Common.Client.Models;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Service.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamETic.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _us;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(
            IUserService us,
            IConfiguration configuration,
            IMapper mapper)
        {
            _mapper = mapper;
            _us = us;
            _configuration = configuration;
        }
        [HttpGet("login")]
        public async Task<WebApiResponse<UserResponse>> Login([FromQuery] LoginRequest request)
        {
            var result = await _us.GetByDefault(x => x.Email == request.Email && x.Password == request.Password);
            if (result != null)
            {
                UserResponse rm = _mapper.Map<UserResponse>(result);
                rm.AccessToken = SetAccessToken(rm);
                return new WebApiResponse<UserResponse>("Success", true, rm);
            }

            return new WebApiResponse<UserResponse>("User Not Found", false);
        }
        private GetAccessToken SetAccessToken(UserResponse response)
        {
            var claims = new List<Claim>//Haklar Listesi
            {
                new Claim(JwtRegisteredClaimNames.Email,response.Email),
                new Claim(JwtRegisteredClaimNames.Jti,response.Id.ToString())
            };
            //JWT
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Tokens:Expires"]));
            var ticks = expires.ToUnixTime();

            var jwtSecurityToken = new JwtSecurityToken(
                                       issuer: _configuration["Tokens:Issuer"],
                                       audience: _configuration["Tokens:Audience"],
                                       claims: claims,
                                       expires: expires,
                                       signingCredentials: signingCredentials);

            return new GetAccessToken
            {
                TokenType = "BilgeAdamAccessToken",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expires = ticks,
                RefreshToken = $"{response.Email}{response.Password}_{ticks}"
            };
        }
    }
}
