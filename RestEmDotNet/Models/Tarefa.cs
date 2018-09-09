using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestEmDotNet.Models
{
    [Table("Tarefas")]
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Titulo { get; set; }
        [MaxLength(200)]
        public string Descricao { get; set; }

    }
}