namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9d7ed6df-8589-4232-afe6-81e4b9cf7824', N'guest@vidly.com', 0, N'APiSze5oTko4MPhmLKRT4tom7RKqsQahU58sl/wI9EulSa33Mnn9oWOAQZP5DRVojA==', N'b3ec8b63-493b-4008-8dd4-7f0aa5c342f1', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com');
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c05a176e-0c2f-4748-91ae-3cf023adc77f', N'admin@vidly.com', 0, N'AEA1virNP6/CGbcbXoQui5YeYM/5sBCwY2g7UJzNzl7YqVnRyz2R8uM+XpdX3B9w5Q==', N'a4d34825-6f1d-4d78-b2bb-11a871ba95ab', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com');
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3c00e18b-e5ad-41ef-a493-2a48e2f0d9b1', N'CanManageMovies');
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c05a176e-0c2f-4748-91ae-3cf023adc77f', N'3c00e18b-e5ad-41ef-a493-2a48e2f0d9b1')
                ");

        }
        
        public override void Down()
        {
        }
    }
}
