namespace проект.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class www : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Photo");
        }
    }
}
