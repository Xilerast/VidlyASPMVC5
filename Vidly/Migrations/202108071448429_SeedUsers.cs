namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8a61319c-60bb-48db-a995-820793b1fd4a', N'admin@vidly.com', 0, N'AC7/O9K925nvzUJA9B5mQjN/TCcVsaL9/+sH1iP2/J/cIEaqrau5QLD1CC2XXgqYHA==', N'a298edd1-2eb4-4174-9eab-7f527bc0522a', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9b42429a-acf6-4870-b2f3-c18227866973', N'guest@vidly.com', 0, N'ALY4eM6Cd9TMzhqaHAV6Zzl+Rp4Dd2BSWPaOFg+XkqSvdCK/5RHaSqSIh/zrsmVshg==', N'2e161224-43c8-4cbb-92af-622dc28adcb6', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'80999854-11de-49af-9323-5de1334bd84e', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8a61319c-60bb-48db-a995-820793b1fd4a', N'80999854-11de-49af-9323-5de1334bd84e')");
        }
        
        public override void Down()
        {
        }
    }
}
