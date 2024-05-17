namespace проект.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        YearOfRelease = c.Int(nullable: false),
                        Genre = c.String(),
                        AgeRestrictions = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfCopiesSold = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfGames = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrencyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CurrencyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Country = c.String(),
                        Password = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        HoursPlayed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "UserId", "dbo.Users");
            DropForeignKey("dbo.Scores", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Games", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Achievements", "GameId", "dbo.Games");
            DropIndex("dbo.Scores", new[] { "UserId" });
            DropIndex("dbo.Scores", new[] { "CurrencyId" });
            DropIndex("dbo.Games", new[] { "CompanyId" });
            DropIndex("dbo.Achievements", new[] { "GameId" });
            DropTable("dbo.Users");
            DropTable("dbo.Scores");
            DropTable("dbo.Currencies");
            DropTable("dbo.Companies");
            DropTable("dbo.Games");
            DropTable("dbo.Achievements");
        }
    }
}
