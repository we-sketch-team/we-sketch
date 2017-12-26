namespace WeSketch.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "WeSketch.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(),
                        ActiveBoard = c.Boolean(),
                        PublicBoard = c.Boolean(),
                        Title = c.String(nullable: false, maxLength: 20),
                        Desription = c.String(maxLength: 50),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("WeSketch.Boards", t => t.Board_Id)
                .Index(t => t.Board_Id);
            
            CreateTable(
                "WeSketch.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
                        UserID = c.Int(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("WeSketch.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("WeSketch.ChatRooms", t => t.ChatRoomId, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ChatRoomId);
            
            CreateTable(
                "WeSketch.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateRegistered = c.DateTime(),
                        ActiveAccount = c.Boolean(),
                        DateOfBirth = c.DateTime(),
                        LockedBoard_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("WeSketch.Boards", t => t.LockedBoard_Id)
                .Index(t => t.Username, unique: true, name: "Username")
                .Index(t => t.Email, unique: true, name: "Email")
                .Index(t => t.LockedBoard_Id);
            
            CreateTable(
                "WeSketch.UserBoards",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        BoardId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.BoardId, t.RoleId })
                .ForeignKey("WeSketch.Boards", t => t.BoardId, cascadeDelete: true)
                .ForeignKey("WeSketch.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BoardId);
            
            CreateTable(
                "WeSketch.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 20),
                        RoleInBoard_UserId = c.Int(nullable: false),
                        RoleInBoard_BoardId = c.Int(nullable: false),
                        RoleInBoard_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("WeSketch.UserBoards", t => new { t.RoleInBoard_UserId, t.RoleInBoard_BoardId, t.RoleInBoard_RoleId })
                .Index(t => new { t.RoleInBoard_UserId, t.RoleInBoard_BoardId, t.RoleInBoard_RoleId });
            
            CreateTable(
                "WeSketch.CompositeShapeElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(storeType: "ntext"),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "WeSketch.ShapeElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(storeType: "ntext"),
                        Type = c.String(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("WeSketch.CompositeShapeElements", t => t.ParentId, cascadeDelete: true)
                .Index(t => t.ParentId);
            
            CreateTable(
                "WeSketch.ChatRoomUsers",
                c => new
                    {
                        ChatRooms = c.Int(nullable: false),
                        Users = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChatRooms, t.Users })
                .ForeignKey("WeSketch.ChatRooms", t => t.ChatRooms, cascadeDelete: true)
                .ForeignKey("WeSketch.Users", t => t.Users, cascadeDelete: true)
                .Index(t => t.ChatRooms)
                .Index(t => t.Users);
            
            CreateTable(
                "WeSketch.CompositeBoardElements",
                c => new
                    {
                        Boards = c.Int(nullable: false),
                        CompositeShapeElements = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boards, t.CompositeShapeElements })
                .ForeignKey("WeSketch.Boards", t => t.Boards, cascadeDelete: true)
                .ForeignKey("WeSketch.CompositeShapeElements", t => t.CompositeShapeElements, cascadeDelete: true)
                .Index(t => t.Boards)
                .Index(t => t.CompositeShapeElements);
            
            CreateTable(
                "WeSketch.UserFavorites",
                c => new
                    {
                        Boards = c.Int(nullable: false),
                        Users = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boards, t.Users })
                .ForeignKey("WeSketch.Boards", t => t.Boards, cascadeDelete: true)
                .ForeignKey("WeSketch.Users", t => t.Users, cascadeDelete: true)
                .Index(t => t.Boards)
                .Index(t => t.Users);
            
            CreateTable(
                "WeSketch.BoardElements",
                c => new
                    {
                        Boards = c.Int(nullable: false),
                        ShapeElements = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boards, t.ShapeElements })
                .ForeignKey("WeSketch.Boards", t => t.Boards, cascadeDelete: true)
                .ForeignKey("WeSketch.ShapeElements", t => t.ShapeElements, cascadeDelete: true)
                .Index(t => t.Boards)
                .Index(t => t.ShapeElements);
            
        }
        
        public override void Down()
        {
            DropForeignKey("WeSketch.BoardElements", "ShapeElements", "WeSketch.ShapeElements");
            DropForeignKey("WeSketch.BoardElements", "Boards", "WeSketch.Boards");
            DropForeignKey("WeSketch.Users", "LockedBoard_Id", "WeSketch.Boards");
            DropForeignKey("WeSketch.UserFavorites", "Users", "WeSketch.Users");
            DropForeignKey("WeSketch.UserFavorites", "Boards", "WeSketch.Boards");
            DropForeignKey("WeSketch.CompositeBoardElements", "CompositeShapeElements", "WeSketch.CompositeShapeElements");
            DropForeignKey("WeSketch.CompositeBoardElements", "Boards", "WeSketch.Boards");
            DropForeignKey("WeSketch.ShapeElements", "ParentId", "WeSketch.CompositeShapeElements");
            DropForeignKey("WeSketch.ChatRooms", "Board_Id", "WeSketch.Boards");
            DropForeignKey("WeSketch.ChatRoomUsers", "Users", "WeSketch.Users");
            DropForeignKey("WeSketch.ChatRoomUsers", "ChatRooms", "WeSketch.ChatRooms");
            DropForeignKey("WeSketch.Messages", "ChatRoomId", "WeSketch.ChatRooms");
            DropForeignKey("WeSketch.Messages", "UserID", "WeSketch.Users");
            DropForeignKey("WeSketch.UserBoards", "UserId", "WeSketch.Users");
            DropForeignKey("WeSketch.Roles", new[] { "RoleInBoard_UserId", "RoleInBoard_BoardId", "RoleInBoard_RoleId" }, "WeSketch.UserBoards");
            DropForeignKey("WeSketch.UserBoards", "BoardId", "WeSketch.Boards");
            DropIndex("WeSketch.BoardElements", new[] { "ShapeElements" });
            DropIndex("WeSketch.BoardElements", new[] { "Boards" });
            DropIndex("WeSketch.UserFavorites", new[] { "Users" });
            DropIndex("WeSketch.UserFavorites", new[] { "Boards" });
            DropIndex("WeSketch.CompositeBoardElements", new[] { "CompositeShapeElements" });
            DropIndex("WeSketch.CompositeBoardElements", new[] { "Boards" });
            DropIndex("WeSketch.ChatRoomUsers", new[] { "Users" });
            DropIndex("WeSketch.ChatRoomUsers", new[] { "ChatRooms" });
            DropIndex("WeSketch.ShapeElements", new[] { "ParentId" });
            DropIndex("WeSketch.Roles", new[] { "RoleInBoard_UserId", "RoleInBoard_BoardId", "RoleInBoard_RoleId" });
            DropIndex("WeSketch.UserBoards", new[] { "BoardId" });
            DropIndex("WeSketch.UserBoards", new[] { "UserId" });
            DropIndex("WeSketch.Users", new[] { "LockedBoard_Id" });
            DropIndex("WeSketch.Users", "Email");
            DropIndex("WeSketch.Users", "Username");
            DropIndex("WeSketch.Messages", new[] { "ChatRoomId" });
            DropIndex("WeSketch.Messages", new[] { "UserID" });
            DropIndex("WeSketch.ChatRooms", new[] { "Board_Id" });
            DropTable("WeSketch.BoardElements");
            DropTable("WeSketch.UserFavorites");
            DropTable("WeSketch.CompositeBoardElements");
            DropTable("WeSketch.ChatRoomUsers");
            DropTable("WeSketch.ShapeElements");
            DropTable("WeSketch.CompositeShapeElements");
            DropTable("WeSketch.Roles");
            DropTable("WeSketch.UserBoards");
            DropTable("WeSketch.Users");
            DropTable("WeSketch.Messages");
            DropTable("WeSketch.ChatRooms");
            DropTable("WeSketch.Boards");
        }
    }
}
