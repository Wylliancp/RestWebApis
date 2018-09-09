using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestWebApiPaginacao.Models
{
    [Table("Cursos")]
    public class Curso
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O titulo do curso de ser preenchida!")]
        [MaxLength(100, ErrorMessage = "O titulo do curso so poderá conter no maximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Insira uma Url para o Curso!")]
        [Url(ErrorMessage = "Insira uma URL válida.")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Insira um Canal para o Curso!")]
        [JsonConverter(typeof(StringEnumConverter))]//ao inves de retorna o indice 0 a 5.. e retornar o nome do Canal ex(Java,DoNet)
        public Canal Canal { get; set; }

        [Required(ErrorMessage = "A data de publicação do curso deve ser preenchida!")]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "A carga horaria do curso deve ser preenchido!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "A carga horaria deve ter no minimo 1º Hora.")]
        public int CargaHoraria { get; set; }

        [JsonIgnore]
        public virtual ICollection<Aula> Aulas { get; set; }
    }
}