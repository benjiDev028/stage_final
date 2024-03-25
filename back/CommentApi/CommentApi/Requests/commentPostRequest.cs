namespace CommentApi.Requests
{
    public class commentPostRequest
    {
        public int? IdAI { get; set; }
        public Guid? IdUser { get; set; } = Guid.Empty;
        public string? content { get; set; } = string.Empty;
        public int? nombreEtoile { get; set; } 


    }
}
