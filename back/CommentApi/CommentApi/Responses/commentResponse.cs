using System.ComponentModel.DataAnnotations;

namespace CommentApi.Responses
{
    public class commentResponse
    {
        [Key]
        public Guid IdComment { get; set; } = Guid.Empty;
        public Guid IdUser { get; set; } = Guid.Empty;
        public Guid IdAI { get; set; }

        public string Content { get; set; } = string.Empty;
        public int NombreEtoile { get; set; } = 0;
        public DateTime Datepublication { get; set; } = DateTime.Now;
    }
}
