namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRentals : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Rentals", "MovieId", "dbo.Movies");
            DropIndex("dbo.Rentals", new[] { "CustomerId" });
            DropIndex("dbo.Rentals", new[] { "MovieId" });
            DropTable("dbo.Rentals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentedOn = c.DateTime(nullable: false),
                        ReturnedOn = c.DateTime(),
                        Customer_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Rentals", "Movie_Id");
            CreateIndex("dbo.Rentals", "Customer_Id");
            AddForeignKey("dbo.Rentals", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
