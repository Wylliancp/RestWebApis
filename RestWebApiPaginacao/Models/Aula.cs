using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWebApiPaginacao.Models
{
    [Table("Aulas")]
    public class Aula
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "O titulo da aula deve ser preenchida.")]
        [MaxLength(50, ErrorMessage = "O titulo da aula deve ter no maximo 50 caracteres.")]
        [MinLength(10, ErrorMessage = "O titulo da aula deve ter no minino 10 caracteres.")]
        public string Titulo { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "A ordem do curso deve ser maior que zero.")]
        public int Ordem { get; set; }

        [JsonIgnore]
        [ForeignKey("Curso")]
        public int IdCurso { get; set; }

        [JsonIgnore]
        public virtual Curso Curso { get; set; }
    }
}