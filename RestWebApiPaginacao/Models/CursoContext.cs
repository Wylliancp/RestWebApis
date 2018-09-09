using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestWebApiPaginacao.Models
{
    public class CursoContext : DbContext
    {

        public CursoContext() : base("curso")//como eu reescrever o contrutor, estou dizendo que meu name de string de conexão e curso
        {

#if DEBUG
            //Database.Log = d => System.Diagnostics.Debug.Write(d);//esse metodo vamos monitorar as consultas sql no console do Visual Studio
#endif
        }
        //apos verificar deverá criar um indice para melhorar a performance da consulta ordenando pela data de publicação
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
    }
}