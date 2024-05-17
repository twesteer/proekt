namespace проект.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_block : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "UserLock_Id", "dbo.UserLocks");
            DropIndex("dbo.Companies", new[] { "UserLock_Id" });
            DropColumn("dbo.Companies", "UserLock_Id");
            DropTable("dbo.UserLocks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserLocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Reason = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Companies", "UserLock_Id", c => c.Int());
            CreateIndex("dbo.Companies", "UserLock_Id");
            AddForeignKey("dbo.Companies", "UserLock_Id", "dbo.UserLocks", "Id");
        }
    }
}
