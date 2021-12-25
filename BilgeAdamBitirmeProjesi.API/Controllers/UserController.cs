using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _us;
        private readonly IMapper _mapper;

        public UserController(
            IUserService us,
            IMapper mapper)
        {
            _us = us;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List User dönüyor.
            return _mapper.Map<List<UserResponse>>(await _us.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(Guid id)
        {
            var user = _mapper.Map<UserResponse>(await _us.GetById(id));
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> PutUser(Guid id, UserRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try
            {
                User entity = await _us.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _us.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<UserResponse>(updatedResult);
            }
            catch (Exception ex)
            {
                if (!await UserExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserResponse>> PostUser(UserRequest request)
        {

            User entity = _mapper.Map<User>(request);
            var insertResult = await _us.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetUser", new { id = insertResult.Id }, _mapper.Map<UserResponse>(insertResult));
            else
                return new UserResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponse>> DeleteUser(Guid id)
        {
            var user = await _us.GetById(id);
            if (user == null)
                return NotFound();
            await _us.Remove(user);
            return _mapper.Map<UserResponse>(user);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<UserResponse>> Activate(Guid id)
        {
            var result = await _us.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<UserResponse>(await _us.GetById(id));
        }

        private async Task<bool> UserExist(Guid id)
        {
            return await _us.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        public async Task<ActionResult<List<UserResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<UserResponse>>(await _us.GetActive().ToListAsync());
        }
    }
}

