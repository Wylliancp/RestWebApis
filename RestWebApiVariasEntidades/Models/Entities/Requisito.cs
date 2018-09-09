using Newtonsoft.Json;

namespace RestWebApiVariasEntidades.Models.Entities
{
    public class Requisito
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        [JsonIgnore]
        public virtual Vaga Vaga { get; set; }
    }
}