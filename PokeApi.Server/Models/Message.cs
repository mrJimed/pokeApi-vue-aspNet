namespace PokeApi.Server.Models
{
    public class Message
    {
        public string ToEmail { get; set; }
        public string Text { get; set; }
        public string? SubjectMessage { get; set; }
    }
}
