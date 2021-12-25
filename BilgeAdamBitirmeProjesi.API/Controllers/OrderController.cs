using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Order;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : BaseApiController<OrderController>
    {
        private readonly IOrderService _os;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderService os,
            IMapper mapper)
        {
            _os = os;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List Order dönüyor.
            return _mapper.Map<List<OrderResponse>>(await _os.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(Guid id)
        {
            var order = _mapper.Map<OrderResponse>(await _os.GetById(id));
            if (order == null)
                return NotFound();
            return order;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResponse>> PutOrder(Guid id, OrderRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try
            {
                Order entity = await _os.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _os.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<OrderResponse>(updatedResult);
            }
            catch (Exception ex)
            {
                if (!await OrderExist(id))
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
        public async Task<ActionResult<OrderResponse>> PostOrder(OrderRequest request)
        {
            Order entity = _mapper.Map<Order>(request);
            var insertResult = await _os.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetOrder", new { id = insertResult.Id }, _mapper.Map<OrderResponse>(insertResult));
            else
                return new OrderResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderResponse>> DeleteOrder(Guid id)
        {
            var order = await _os.GetById(id);
            if (order == null)
                return NotFound();
            await _os.Remove(order);
            return _mapper.Map<OrderResponse>(order);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<OrderResponse>> Activate(Guid id)
        {
            var result = await _os.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<OrderResponse>(await _os.GetById(id));
        }

        private async Task<bool> OrderExist(Guid id)
        {
            return await _os.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        public async Task<ActionResult<List<OrderResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<OrderResponse>>(await _os.GetActive().ToListAsync());
        }

        [HttpGet("orderiddenbul")]
        public async Task<ActionResult<OrderResponse>> OrderiddenBul(Guid userid)
        {
            foreach (var item in _mapper.Map<List<OrderResponse>>(await _os.TableNoTracking.ToListAsync()))
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
