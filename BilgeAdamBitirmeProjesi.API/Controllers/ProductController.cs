using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Product;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : BaseApiController<ProductController>
    {
        private readonly IProductService _ps;
        private readonly IMapper _mapper;

        public ProductController(
            IProductService ps,
            IMapper mapper)
        {
            _ps = ps;
            _mapper = mapper;

        }
        //Buraya bakılacak
        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List Product dönüyor.
            return _mapper.Map<List<ProductResponse>>(await _ps.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductResponse>(await _ps.GetById(id));
            if (product == null)
                return NotFound();
            return product;
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductResponse>> PutProduct(Guid id, ProductRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try
            {
                Product entity = await _ps.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _ps.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<ProductResponse>(updatedResult);
            }
            catch (Exception ex)
            {
                if (!await ProductExist(id))
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
        public async Task<ActionResult<ProductResponse>> PostProduct(ProductRequest request)
        {
            Product entity = _mapper.Map<Product>(request);
            var insertResult = await _ps.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetProduct", new { id = insertResult.Id }, _mapper.Map<ProductResponse>(insertResult));
            else
                return new ProductResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(Guid id)
        {
            var product = await _ps.GetById(id);
            if (product == null)
                return NotFound();
            await _ps.Remove(product);
            return _mapper.Map<ProductResponse>(product);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<ProductResponse>> Activate(Guid id)
        {
            var result = await _ps.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<ProductResponse>(await _ps.GetById(id));
        }

        private async Task<bool> ProductExist(Guid id)
        {
            return await _ps.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<ProductResponse>>(await _ps.GetActive().ToListAsync());
        }


        [HttpGet("GetByCategoryId/{categoryId}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductResponse>>> GetByCategoryId(Guid categoryId)
        {
            return _mapper.Map<List<ProductResponse>>(_ps.Default(x => x.CategoryId == categoryId));

        }
    }
}
