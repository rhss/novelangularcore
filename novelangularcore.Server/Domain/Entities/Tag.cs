namespace novelangularcore.Server.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ICollection<NovelTag> NovelTags { get; set; }
    }
}
