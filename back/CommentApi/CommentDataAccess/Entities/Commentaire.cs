using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentDataAccess.Entities
{
    public class Commentaire
    {
        [Key]
        public Guid IdComment { get; set; } = Guid.Empty;
        public int IdAI { get; set; }
        public Guid IdUser { get; set; } = Guid.Empty;
        public string Content { get; set; } = string.Empty;
        public int NombreEtoile { get; set; } = 0;
        public DateTime Datepublication { get; set; } = DateTime.Now;


    }
}
