namespace RestWebApiVariasEntidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniciar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vagas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        LocalTrabalho = c.String(),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.Requisitos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100),
                        Vaga_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vagas", t => t.Vaga_Id)
                .Index(t => t.Vaga_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisitos", "Vaga_Id", "dbo.Vagas");
            DropForeignKey("dbo.Vagas", "EmpresaId", "dbo.Empresas");
            DropIndex("dbo.Requisitos", new[] { "Vaga_Id" });
            DropIndex("dbo.Vagas", new[] { "EmpresaId" });
            DropTable("dbo.Requisitos");
            DropTable("dbo.Vagas");
            DropTable("dbo.Empresas");
        }
    }
}
