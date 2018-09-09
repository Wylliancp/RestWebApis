using RestWebApiVariasEntidades.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace RestWebApiVariasEntidades.Models.Mapping
{
    public class EmpresaMapping : EntityTypeConfiguration<Empresa>
    {

        public EmpresaMapping()
        {

            ToTable("Empresas");
            HasKey(x => x.Id);
            Property(x => x.Nome).IsRequired().HasMaxLength(100);
        }
    }
}