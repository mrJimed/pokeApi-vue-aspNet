using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeApiV2.Server.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Column("username", TypeName = "varchar")]
        public string? Username { get; set; }

        [Column("email", TypeName = "varchar")]
        public string? Email { get; set; }

        [Column("password", TypeName = "varchar")]
        public string? Password { get; set; }

        [Column("salt", TypeName = "varchar")]
        public string? Salt { get; set; }
    }
}
