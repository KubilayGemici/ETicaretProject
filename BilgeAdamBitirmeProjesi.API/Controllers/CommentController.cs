using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BilgeAdamBitirmeProjesi.Common.DTOs.Comment;
using BilgeAdamBitirmeProjesi.Common.DTOs.User;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Service.Service.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamBitirmeProjesi.API.Controllers
{
    [Route("comment")]
    [ApiController]
    public class CommentController : BaseApiController<CommentController>
    {
        private readonly ICommentService _ccs;
        private readonly IMapper _mapper;

        public CommentController(
            ICommentService ccs,
            IMapper mapper)
        {
            _ccs = ccs;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<List<CommentResponse>>> GetCategories()
        {
            UserResponse current = WorkContext.CurrentUser;
            //List Comment dönüyor.
            return _mapper.Map<List<CommentResponse>>(await _ccs.TableNoTracking.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponse>> GetComment(Guid id)
        {
            var comment = _mapper.Map<CommentResponse>(await _ccs.GetById(id));
            if (comment == null)
                return NotFound();
            return comment;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentResponse>> PutComment(Guid id, CommentRequest request)
        {
            if (id != request.Id)
                //BadRequest 404
                return BadRequest();
            try
            {
                Comment entity = await _ccs.GetById(id);
                if (entity == null)
                    return NotFound();
                //Uİ dan Database gidiyorum.
                _mapper.Map(request, entity);

                var updatedResult = await _ccs.Update(entity);
                if (updatedResult != null)
                    return _mapper.Map<CommentResponse>(updatedResult);
            }
            catch (Exception ex)
            {
                if (!await CommentExist(id))
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
        public async Task<ActionResult<CommentResponse>> PostComment(CommentRequest request)
        {
            Comment entity = _mapper.Map<Comment>(request);
            var insertResult = await _ccs.Add(entity);
            if (insertResult != null)
                return CreatedAtAction("GetComment", new { id = insertResult.Id }, _mapper.Map<CommentResponse>(insertResult));
            else
                return new CommentResponse();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentResponse>> DeleteComment(Guid id)
        {
            var comment = await _ccs.GetById(id);
            if (comment == null)
                return NotFound();
            await _ccs.Remove(comment);
            return _mapper.Map<CommentResponse>(comment);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<CommentResponse>> Activate(Guid id)
        {
            var result = await _ccs.Activate(id);
            //Databaseden son hali çekilir
            return _mapper.Map<CommentResponse>(await _ccs.GetById(id));
        }

        private async Task<bool> CommentExist(Guid id)
        {
            return await _ccs.Any(e => e.Id == id);
        }

        [HttpGet("getactive")]
        public async Task<ActionResult<List<CommentResponse>>> GetActive()
        {
            //Tolistasync gördüğüm anda Query çalışıyor demektir.
            return _mapper.Map<List<CommentResponse>>(await _ccs.GetActive().ToListAsync());
        }
    }
}
