namespace BankMidterm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        gender = c.Int(nullable: false),
                        IdentityNumber = c.String(nullable: false, maxLength: 11),
                        BirthDate = c.DateTime(nullable: false),
                        telephone = c.String(maxLength: 50),
                        Email = c.String(),
                        City_Id = c.Int(),
                        Country_Id = c.Int(),
                        Voucher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.Vouchers", t => t.Voucher_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Country_Id)
                .Index(t => t.Voucher_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RelationToClient = c.Int(nullable: false),
                        IdentityNumber = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "Voucher_Id", "dbo.Vouchers");
            DropForeignKey("dbo.Clients", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Clients", "City_Id", "dbo.Cities");
            DropIndex("dbo.Clients", new[] { "Voucher_Id" });
            DropIndex("dbo.Clients", new[] { "Country_Id" });
            DropIndex("dbo.Clients", new[] { "City_Id" });
            DropTable("dbo.Vouchers");
            DropTable("dbo.Countries");
            DropTable("dbo.Clients");
            DropTable("dbo.Cities");
        }
    }
}
