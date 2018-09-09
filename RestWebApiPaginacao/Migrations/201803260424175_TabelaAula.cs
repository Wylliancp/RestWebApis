namespace RestWebApiPaginacao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaAula : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aulas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 50),
                        Ordem = c.Int(nullable: false),
                        IdCurso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cursos", t => t.IdCurso, cascadeDelete: true)
                .Index(t => t.IdCurso);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aulas", "IdCurso", "dbo.Cursos");
            DropIndex("dbo.Aulas", new[] { "IdCurso" });
            DropTable("dbo.Aulas");
        }
    }
}
