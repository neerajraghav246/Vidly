namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGeneres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Generes (Id,Name) VALUES(1,'Family')");
            Sql("INSERT INTO Generes (Id,Name) VALUES(2,'Action')");
            Sql("INSERT INTO Generes (Id,Name) VALUES(3,'Comedy')");
            Sql("INSERT INTO Generes (Id,Name) VALUES(4,'Romance')");
            Sql("INSERT INTO Generes (Id,Name) VALUES(5,'Thriller')");
        }

        public override void Down()
        {
        }
    }
}
