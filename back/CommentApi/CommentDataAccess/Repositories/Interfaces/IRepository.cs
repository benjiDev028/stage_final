using CommentDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentDataAccess.Repositories.Interfaces
{
    public interface IRepository
    {
        //getallcomment
        //getalliduser
        //getbyidcomoment

        //cretaecomment
        //updatecommetby

        Task<bool> CreateCommentAsync( Commentaire commentaire);
        Task<Commentaire> UpdateCommentAsync(Commentaire commentaire);
        Task<bool> DeleteCommentAsync(Guid commentaire);
        Task<Commentaire> GetCommentAsync(Guid id);
        Task<List<Commentaire>> GetAllCommentsAsync();
       
    }
}
