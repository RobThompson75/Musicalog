namespace Musicalog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ArtistId = c.Int(nullable: false),
                        RecordLabelId = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artist", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.RecordLabel", t => t.RecordLabelId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.RecordLabelId);
            
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecordLabel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MediaType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AlbumMediaType",
                c => new
                    {
                        AlbumId = c.Int(nullable: false),
                        MediaTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlbumId, t.MediaTypeId })
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.MediaType", t => t.MediaTypeId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.MediaTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumMediaType", "MediaTypeId", "dbo.MediaType");
            DropForeignKey("dbo.AlbumMediaType", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.Album", "RecordLabelId", "dbo.RecordLabel");
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropIndex("dbo.AlbumMediaType", new[] { "MediaTypeId" });
            DropIndex("dbo.AlbumMediaType", new[] { "AlbumId" });
            DropIndex("dbo.Album", new[] { "RecordLabelId" });
            DropIndex("dbo.Album", new[] { "ArtistId" });
            DropTable("dbo.AlbumMediaType");
            DropTable("dbo.MediaType");
            DropTable("dbo.RecordLabel");
            DropTable("dbo.Artist");
            DropTable("dbo.Album");
        }
    }
}
