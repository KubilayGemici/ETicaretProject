using BilgeAdamBitirmeProjesi.Common.DTOs.Comment;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.APIs
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface ICommentApi
    {
        [Get("/comment")]
        Task<ApiResponse<List<CommentResponse>>> List();

        [Get("/comment/{id}")]
        Task<ApiResponse<CommentResponse>> Get(Guid id);

        [Put("/comment/{id}")]
        Task<ApiResponse<CommentResponse>> Put(Guid id, CommentRequest request);

        [Post("/comment")]
        Task<ApiResponse<CommentResponse>> Post(CommentRequest request);

        [Delete("/comment/{id}")]
        Task<ApiResponse<CommentResponse>> Delete(Guid id);

        [Get("/comment/activate/{id}")]
        Task<ApiResponse<CommentResponse>> Activate(Guid id);
    }
}
