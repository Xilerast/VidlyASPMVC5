namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNameOfMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Pay as you go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET Name = 'Quarterly' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET Name = 'Annual' WHERE Id = 1");
        }
        
        public override void Down()
        {
        }
    }
}
