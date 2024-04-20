namespace PokeApiV2.Server.Models
{
    public record class Message(string ToEmail, string? Subject, string? Text);
}
