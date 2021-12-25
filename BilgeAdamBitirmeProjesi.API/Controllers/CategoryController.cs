using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Category;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : BaseApiController<CategoryController>
    {
        private readonly ICategoryService _cs;
        private readonly IMapper _mapper;

        public CategoryController(
            ICategoryService cs, 
            IMapper mapper)
        {
            _cs = cs;
            _mapper = mapper;
           
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List Category dönüyor.
            return _mapper.Map<List<CategoryResponse>>(await _cs.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(Guid id)
        {
            var category = _mapper.Map<CategoryResponse>(await _cs.GetById(id));
            if (category == null)
                return NotFound();
            return category;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponse>> PutCategory(Guid id, CategoryRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try       
            {
                Category entity = await _cs.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _cs.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<CategoryResponse>(updatedResult);
            }
            catch (Exception ex)
            {
                if (!await CategoryExist(id))
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
        public async Task<ActionResult<CategoryResponse>> PostCategory(CategoryRequest request)
        {
            Category entity = _mapper.Map<Category>(request);
            var insertResult = await _cs.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetCategory", new { id = insertResult.Id }, _mapper.Map<CategoryResponse>(insertResult));
            else
                return new CategoryResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryResponse>> DeleteCategory(Guid id)
        {
            var category = await _cs.GetById(id);
            if (category == null)
                return NotFound();
            await _cs.Remove(category);
            return _mapper.Map<CategoryResponse>(category);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<CategoryResponse>> Activate(Guid id)
        {
            var result = await _cs.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<CategoryResponse>(await _cs.GetById(id));
        }

        private async Task<bool> CategoryExist(Guid id)
        {
            return await _cs.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<CategoryResponse>>(await _cs.GetActive().ToListAsync());
        }
    }
}
