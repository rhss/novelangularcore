namespace novelangularcore.Server.Domain.Entities
{
    public class Autor : Usuario
    {
        public Usuario Usuario { get; set; }//herda quase tudo de usuario
        public int UsuarioId { get; set; }
    }
}
