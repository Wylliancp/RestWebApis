using RestWebApiVariasEntidades.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace RestWebApiVariasEntidades.Models.Mapping
{
    public class VagaMapping : EntityTypeConfiguration<Vaga>
    {

        public VagaMapping()
        {
            ToTable("Vagas");
            HasKey(x => x.Id);
            Property(x => x.Titulo).IsRequired().HasMaxLength(100);
        }
    }
}