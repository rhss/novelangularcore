namespace novelangularcore.Server.Domain.Entities
{
    public class Administrador
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }//apesar de compartilhar campos com usuario separado por segurança
        public string Salt { get; set; }
    }
}
