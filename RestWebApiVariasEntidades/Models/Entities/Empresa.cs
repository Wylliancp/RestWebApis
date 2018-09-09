using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWebApiVariasEntidades.Models.Entities
{
    public class Empresa
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vaga> Vagas { get; set; }
    }
}