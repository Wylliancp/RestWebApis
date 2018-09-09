using RestWebApiVariasEntidades.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace RestWebApiVariasEntidades.Models.Mapping
{
    public class RequisitoMapping : EntityTypeConfiguration<Requisito>
    {//A classe de Mapping da classe expecifica herda de EnntityTypeConfiguration e fala de qual tipo ex: objeto Requisito

        public RequisitoMapping()//O mapeamento da classe para modelo, se originaliza do construtor da classe
        {

            ToTable("Requisitos");//nome da tabela.
            HasKey(x => x.Id);//fala qual e o id da tabela, o entity já entendi que a propiedade tiver nome id, ele automaticamente será chave da tabela.
            Property(x => x.Descricao).IsRequired().HasMaxLength(100);//aqui eu estou determinando se e not null e valor maximo da propriedade.
        }
    }
}