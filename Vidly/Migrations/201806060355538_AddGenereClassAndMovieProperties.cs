namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenereClassAndMovieProperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Generes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Short(nullable: false));
            AddColumn("dbo.Movies", "GenereId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Movies", "GenereId");
            AddForeignKey("dbo.Movies", "GenereId", "dbo.Generes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "GenereId", "dbo.Generes");
            DropIndex("dbo.Movies", new[] { "GenereId" });
            DropColumn("dbo.Movies", "GenereId");
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropTable("dbo.Generes");
        }
    }
}
