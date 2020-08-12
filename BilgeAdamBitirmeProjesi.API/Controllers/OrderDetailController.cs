using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.OrderDetail;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.OrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("orderdetail")]
    [ApiController]
    public class OrderDetailController : BaseApiController<OrderDetailController>
    {
        private readonly IOrderDetailService _ods;
        private readonly IMapper _mapper;

        public OrderDetailController(
            IOrderDetailService ods,
            IMapper mapper)
        {
            _ods = ods;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<OrderDetailResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List OrderDetail dönüyor.
            return _mapper.Map<List<OrderDetailResponse>>(await _ods.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailResponse>> GetOrderDetail(Guid id)
        {
            var orderdetail = _mapper.Map<OrderDetailResponse>(await _ods.GetById(id));
            if (orderdetail == null)
                return NotFound();
            return orderdetail;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDetailResponse>> PutOrderDetail(Guid id, OrderDetailRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try
            {
                OrderDetail entity = await _ods.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _ods.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<OrderDetailResponse>(updatedResult);
            }
            catch (Exception ex)
            {
                if (!await OrderDetailExist(id))
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
        public async Task<ActionResult<OrderDetailResponse>> PostOrderDetail(OrderDetailRequest request)
        {
            OrderDetail entity = _mapper.Map<OrderDetail>(request);
            var insertResult = await _ods.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetOrderDetail", new { id = insertResult.Id }, _mapper.Map<OrderDetailResponse>(insertResult));
            else
                return new OrderDetailResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDetailResponse>> DeleteOrderDetail(Guid id)
        {
            var orderdetail = await _ods.GetById(id);
            if (orderdetail == null)
                return NotFound();
            await _ods.Remove(orderdetail);
            return _mapper.Map<OrderDetailResponse>(orderdetail);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<OrderDetailResponse>> Activate(Guid id)
        {
            var result = await _ods.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<OrderDetailResponse>(await _ods.GetById(id));
        }

        private async Task<bool> OrderDetailExist(Guid id)
        {
            return await _ods.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        public async Task<ActionResult<List<OrderDetailResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<OrderDetailResponse>>(await _ods.GetActive().ToListAsync());
        }
    }
}
