namespace novelangularcore.Server.Domain.Entities
{
    public class Novel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public string Descricao { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        public ICollection<NovelTag> NovelTags { get; set; }
    }
}
