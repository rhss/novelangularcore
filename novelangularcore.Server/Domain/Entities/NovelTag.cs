namespace novelangularcore.Server.Domain.Entities
{
    public class NovelTag
    {
        public int Id { get; set; }
        public int IdTag { get; set; }
        public int IdNovel { get; set; }

        public Tag Tag { get; set; }
        public Novel Novel { get; set; }
    }
}
