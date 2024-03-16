namespace CommentApi.Requests
{
    public class commentUpdateRequest
    {
        public Guid IdComment { get; set; } = Guid.Empty;
        public int IdAI { get; set; }
        public Guid IdUser { get; set; } = Guid.Empty;
        public string Content { get; set; } = string.Empty;
        public int NombreEtoile { get; set; } = 0;
    }
}
