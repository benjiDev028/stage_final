using AutoMapper;
using CommentBusinessLogic.DTO;
using CommentBusinessLogic.Services.Interfaces;
using CommentDataAccess.Entities;
using CommentDataAccess.Repositories.Implementations;
using CommentDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentBusinessLogic.Services.Implementations
{
    public class commentService : ICommentService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public commentService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateCommentAsync(CommentDto commentaire)
        {
            try
            {
                var newComment = _mapper.Map<Commentaire>(commentaire);
                var isCreated = await _repository.CreateCommentAsync(newComment);

                return isCreated;
            }catch(Exception e)
            {
                throw e;
            }
        }

        public Task<bool> DeleteCommentAsync(Guid id)
        {
            try
            {
                var commentDeleted = _repository.DeleteCommentAsync(id);
                return commentDeleted;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task<List<CommentDto>> GetAllCommentsAsync()
        {
            try
            {
                var allComments = await _repository.GetAllCommentsAsync();
                return _mapper.Map<List<CommentDto>>(allComments);

            }catch(Exception e)
            {
                throw e;
            }

        }

        public async Task<CommentDto> GetCommentAsync(Guid id)
        {
            try
            {
                var commentSelected = await _repository.GetCommentAsync(id);
                return _mapper.Map<CommentDto>(commentSelected);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task<CommentDto> UpdateCommentAsync(CommentDto commentaire)
        {
            try
            {
                var comment = _mapper.Map<Commentaire>(commentaire);

                var commentUp = await _repository.UpdateCommentAsync(comment.idComment);
                return  _mapper.Map<CommentDto>(commentUp);

            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
