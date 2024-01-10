using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeApi.Server.Models
{
    [Table("user")]
    public class User
    {
        [Column("user_id")]
        [Key]
        public Guid UserId { get; private set; } = Guid.NewGuid();

        [Column("username")]
        public string? Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("salt")]
        public string? Salt { get; set; }
    }
}
