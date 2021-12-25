using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Cart;
using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartController : BaseApiController<CartController>
    {
        private readonly ICartService _ccs;
        private readonly IMapper _mapper;

        public CartController(
            ICartService ccs,
            IMapper mapper)
        {
            _ccs = ccs;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<CartResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List Cart dönüyor.
            return _mapper.Map<List<CartResponse>>(await _ccs.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartResponse>> GetCart(Guid id)
        {
            var cart = _mapper.Map<CartResponse>(await _ccs.GetById(id));
            if (cart == null)
                return NotFound();
            return cart;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CartResponse>> PutCart(Guid id, CartRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try
            {
                Cart entity = await _ccs.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _ccs.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<CartResponse>(updatedResult);
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
        public async Task<ActionResult<CartResponse>> PostCart(CartRequest request)
        {
            Cart entity = _mapper.Map<Cart>(request);
            var insertResult = await _ccs.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetCart", new { id = insertResult.Id }, _mapper.Map<CartResponse>(insertResult));
            else
                return new CartResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CartResponse>> DeleteCart(Guid id)
        {
            var cart = await _ccs.GetById(id);
            if (cart == null)
                return NotFound();
            await _ccs.Remove(cart);
            return _mapper.Map<CartResponse>(cart);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<CartResponse>> Activate(Guid id)
        {
            var result = await _ccs.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<CartResponse>(await _ccs.GetById(id));
        }

        private async Task<bool> CartExist(Guid id)
        {
            return await _ccs.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        public async Task<ActionResult<List<CartResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<CartResponse>>(await _ccs.GetActive().ToListAsync());
        }

        [HttpGet("cartiddenbul")]
        public async Task<ActionResult<CartResponse>> UseriddenCartidBul(Guid userid)
        {
            foreach (var item in _mapper.Map<List<CartResponse>>(await _ccs.TableNoTracking.ToListAsync()))
            {
                if (item.UserId == userid)
                {
                    return _mapper.Map<CartResponse>(item);
                }
            }
            return null;
        }

        [HttpGet("orderiddenbul")]
        public async Task<ActionResult<OrderResponse>> OrderiddenBul(Guid userid)
        {
            foreach (var item in _mapper.Map<List<OrderResponse>>(await _ccs.TableNoTracking.ToListAsync()))
            {
                if (item.UserId == userid)
                {
                    return _mapper.Map<OrderResponse>(item);
                }
            }
            return null;
        }
    }
}
