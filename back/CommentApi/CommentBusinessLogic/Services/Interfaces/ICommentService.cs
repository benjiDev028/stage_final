using CommentBusinessLogic.DTO;
using CommentDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentBusinessLogic.Services.Interfaces
{
    public interface ICommentService
    {
        Task<bool> CreateCommentAsync(CommentDto commentaire);
        Task<CommentDto> UpdateCommentAsync(CommentDto commentaire);
        Task<bool> DeleteCommentAsync(Guid id);
        Task<CommentDto> GetCommentAsync(Guid id);
        Task<List<CommentDto>> GetAllCommentsAsync();

    }
}
