using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentBusinessLogic.DTO
{
    public class CommentDto
    {
        [Key]
        public Guid idComment { get; set; } = Guid.Empty;
        public Guid idUser { get; set; } = Guid.Empty;
        public string Content { get; set; } = string.Empty;
        public int nombreEtoile { get; set; } = 0;
        public DateTime Datepublication { get; set; } = DateTime.Now;

    }
}
