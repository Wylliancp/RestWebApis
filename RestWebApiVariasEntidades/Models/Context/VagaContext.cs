using RestWebApiVariasEntidades.Models.Entities;
using RestWebApiVariasEntidades.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestWebApiVariasEntidades.Models.Context
{
    public class VagaContext : DbContext
    {
        public VagaContext() : base("DbVagas")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Vaga>(new VagaMapping());
            modelBuilder.Configurations.Add<Empresa>(new EmpresaMapping());
            modelBuilder.Configurations.Add<Requisito>(new RequisitoMapping());
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Requisito> Requisito { get; set; }


        
    }
}