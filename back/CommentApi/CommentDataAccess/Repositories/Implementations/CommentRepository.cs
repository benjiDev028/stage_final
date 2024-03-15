using CommentDataAccess.Entities;
using CommentDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentDataAccess.Repositories.Implementations
{
    public class CommentRepository : IRepository
    {
        private readonly CommentContext _commentContext;

        public CommentRepository(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }
        public async Task<bool> CreateCommentAsync(Commentaire commentaire)
        {
            _commentContext.Commentaires.Add(commentaire);
            await _commentContext.SaveChangesAsync();
            return true;
        }

        public  async Task<bool> DeleteCommentAsync(Guid id)
        {
            var commentDeleted = await _commentContext.Commentaires.FindAsync(id);
            _commentContext.Commentaires.Remove(commentDeleted);
            await _commentContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Commentaire>> GetAllCommentsAsync()
        {
            return await _commentContext.Commentaires.ToListAsync();

        }

        public async Task<Commentaire?> GetCommentAsync(Guid id)
        {
            var  commentSelected = await  _commentContext.Commentaires.FindAsync(id);
            return commentSelected;   
        }

        public async Task<Commentaire> UpdateCommentAsync(Commentaire commentaire)
        {
            var commentUp = await _commentContext.Commentaires.FindAsync(commentaire);

            commentUp.Content = commentaire.Content;
            commentUp.NombreEtoile = commentaire.NombreEtoile;
            commentUp.Datepublication = DateTime.Now;

             _commentContext.Commentaires.Update(commentUp);
            await _commentContext.SaveChangesAsync();
            return  commentUp;
        }

        public Task<Commentaire> UpdateCommentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

       
    }
}
