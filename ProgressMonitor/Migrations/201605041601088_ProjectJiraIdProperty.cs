namespace ProgressMonitor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectJiraIdProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "JiraId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "JiraId");
        }
    }
}
