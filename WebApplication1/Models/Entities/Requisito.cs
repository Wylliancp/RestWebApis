namespace WebApplication1.Models.Entities
{
    public class Requisito
    {

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual Vaga Vaga { get; set; }
    }
}