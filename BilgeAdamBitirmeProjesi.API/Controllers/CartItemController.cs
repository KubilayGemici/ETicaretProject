using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.CartItem;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.CartItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("cartıtem")]
    [ApiController]
    public class CartItemController : BaseApiController<CartItemController>
    {
        private readonly ICartItemService _cıs;
        private readonly IMapper _mapper;

        public CartItemController(
            ICartItemService cıs,
            IMapper mapper)
        {
            _cıs = cıs;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<CartItemResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List Cart dönüyor.
            return _mapper.Map<List<CartItemResponse>>(await _cıs.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemResponse>> GetCartItem(Guid id)
        {
            var cartıtem = _mapper.Map<CartItemResponse>(await _cıs.GetById(id));
            if (cartıtem == null)
                return NotFound();
            return cartıtem;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CartItemResponse>> PutCartItem(Guid id, CartItemRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try
            {
                CartItem entity = await _cıs.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _cıs.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<CartItemResponse>(updatedResult);
            }
            catch (Exception ex)
            {
                if (!await CartExist(id))
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
        public async Task<ActionResult<CartItemResponse>> PostCartItem(CartItemRequest request)
        {
            CartItem entity = _mapper.Map<CartItem>(request);
            var insertResult = await _cıs.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetCart", new { id = insertResult.Id }, _mapper.Map<CartItemResponse>(insertResult));
            else
                return new CartItemResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CartItemResponse>> DeleteCartItem(Guid id)
        {
            var cartıtem = await _cıs.GetById(id);
            if (cartıtem == null)
                return NotFound();
            await _cıs.Remove(cartıtem);
            return _mapper.Map<CartItemResponse>(cartıtem);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<CartItemResponse>> Activate(Guid id)
        {
            var result = await _cıs.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<CartItemResponse>(await _cıs.GetById(id));
        }

        private async Task<bool> CartExist(Guid id)
        {
            return await _cıs.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        public async Task<ActionResult<List<CartItemResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<CartItemResponse>>(await _cıs.GetActive().ToListAsync());
        }
    }
}
