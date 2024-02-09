namespace PokeApiV2.Server.Models
{
    public class Pokemon
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int AttackPower { get; set; }
        public string ImageUrl { get; set; }
    }
}
