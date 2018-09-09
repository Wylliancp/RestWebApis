using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Entities
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Salario { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Today;
        public string LocalTrabalho { get; set; }
        public int EmpresaId { get; set; }

        [JsonIgnore]
        public virtual Empresa Anunciante { get; set; }
        
        public ICollection<Requisito> Requisitos { get; set; }
    }
}