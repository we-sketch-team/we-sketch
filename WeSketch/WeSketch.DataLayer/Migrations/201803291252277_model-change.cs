namespace WeSketch.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("WeSketch.Messages", "UserID", "WeSketch.Users");
            DropForeignKey("WeSketch.Messages", "ChatRoomId", "WeSketch.ChatRooms");
            DropForeignKey("WeSketch.ChatRoomUsers", "ChatRooms", "WeSketch.ChatRooms");
            DropForeignKey("WeSketch.ChatRoomUsers", "Users", "WeSketch.Users");
            DropForeignKey("WeSketch.ChatRooms", "Board_Id", "WeSketch.Boards");
            DropForeignKey("WeSketch.Users", "LockedBoard_Id", "WeSketch.Boards");
            DropIndex("WeSketch.ChatRooms", new[] { "Board_Id" });
            DropIndex("WeSketch.Messages", new[] { "UserID" });
            DropIndex("WeSketch.Messages", new[] { "ChatRoomId" });
            DropIndex("WeSketch.Users", new[] { "LockedBoard_Id" });
            DropIndex("WeSketch.ChatRoomUsers", new[] { "ChatRooms" });
            DropIndex("WeSketch.ChatRoomUsers", new[] { "Users" });
            AddColumn("WeSketch.Boards", "Password", c => c.String());
            DropColumn("WeSketch.Boards", "PublicBoard");
            DropColumn("WeSketch.Users", "LockedBoard_Id");
            DropTable("WeSketch.ChatRooms");
            DropTable("WeSketch.Messages");
            DropTable("WeSketch.ChatRoomUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "WeSketch.ChatRoomUsers",
                c => new
                    {
                        ChatRooms = c.Int(nullable: false),
                        Users = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChatRooms, t.Users });
            
            CreateTable(
                "WeSketch.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
                        UserID = c.Int(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "WeSketch.ChatRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActiveChat = c.Boolean(),
                        DateCreated = c.DateTime(),
                        Board_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("WeSketch.Users", "LockedBoard_Id", c => c.Int());
            AddColumn("WeSketch.Boards", "PublicBoard", c => c.Boolean());
            DropColumn("WeSketch.Boards", "Password");
            CreateIndex("WeSketch.ChatRoomUsers", "Users");
            CreateIndex("WeSketch.ChatRoomUsers", "ChatRooms");
            CreateIndex("WeSketch.Users", "LockedBoard_Id");
            CreateIndex("WeSketch.Messages", "ChatRoomId");
            CreateIndex("WeSketch.Messages", "UserID");
            CreateIndex("WeSketch.ChatRooms", "Board_Id");
            AddForeignKey("WeSketch.Users", "LockedBoard_Id", "WeSketch.Boards", "Id");
            AddForeignKey("WeSketch.ChatRooms", "Board_Id", "WeSketch.Boards", "Id");
            AddForeignKey("WeSketch.ChatRoomUsers", "Users", "WeSketch.Users", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.ChatRoomUsers", "ChatRooms", "WeSketch.ChatRooms", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.Messages", "ChatRoomId", "WeSketch.ChatRooms", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.Messages", "UserID", "WeSketch.Users", "Id", cascadeDelete: true);
        }
    }
}
