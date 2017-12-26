namespace WeSketch.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationshipfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("WeSketch.Roles", new[] { "RoleInBoard_UserId", "RoleInBoard_BoardId", "RoleInBoard_RoleId" }, "WeSketch.UserBoards");
            DropForeignKey("WeSketch.ShapeElements", "ParentId", "WeSketch.CompositeShapeElements");
            DropForeignKey("WeSketch.CompositeBoardElements", "Boards", "WeSketch.Boards");
            DropForeignKey("WeSketch.CompositeBoardElements", "CompositeShapeElements", "WeSketch.CompositeShapeElements");
            DropForeignKey("WeSketch.UserFavorites", "Boards", "WeSketch.Boards");
            DropForeignKey("WeSketch.UserFavorites", "Users", "WeSketch.Users");
            DropForeignKey("WeSketch.BoardElements", "Boards", "WeSketch.Boards");
            DropForeignKey("WeSketch.BoardElements", "ShapeElements", "WeSketch.ShapeElements");
            DropIndex("WeSketch.Roles", new[] { "RoleInBoard_UserId", "RoleInBoard_BoardId", "RoleInBoard_RoleId" });
            DropIndex("WeSketch.ShapeElements", new[] { "ParentId" });
            DropIndex("WeSketch.CompositeBoardElements", new[] { "Boards" });
            DropIndex("WeSketch.CompositeBoardElements", new[] { "CompositeShapeElements" });
            DropIndex("WeSketch.UserFavorites", new[] { "Boards" });
            DropIndex("WeSketch.UserFavorites", new[] { "Users" });
            DropIndex("WeSketch.BoardElements", new[] { "Boards" });
            DropIndex("WeSketch.BoardElements", new[] { "ShapeElements" });
            DropPrimaryKey("WeSketch.UserBoards");
            AddColumn("WeSketch.Boards", "Content", c => c.String(storeType: "ntext"));
            AddColumn("WeSketch.UserBoards", "Role", c => c.String(nullable: false, maxLength: 1000));
            AddColumn("WeSketch.UserBoards", "IsFavoriteToUser", c => c.Boolean(nullable: false));
            AddPrimaryKey("WeSketch.UserBoards", new[] { "UserId", "BoardId" });
            DropColumn("WeSketch.UserBoards", "RoleId");
            DropTable("WeSketch.Roles");
            DropTable("WeSketch.CompositeShapeElements");
            DropTable("WeSketch.ShapeElements");
            DropTable("WeSketch.CompositeBoardElements");
            DropTable("WeSketch.UserFavorites");
            DropTable("WeSketch.BoardElements");
        }
        
        public override void Down()
        {
            CreateTable(
                "WeSketch.BoardElements",
                c => new
                    {
                        Boards = c.Int(nullable: false),
                        ShapeElements = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boards, t.ShapeElements });
            
            CreateTable(
                "WeSketch.UserFavorites",
                c => new
                    {
                        Boards = c.Int(nullable: false),
                        Users = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boards, t.Users });
            
            CreateTable(
                "WeSketch.CompositeBoardElements",
                c => new
                    {
                        Boards = c.Int(nullable: false),
                        CompositeShapeElements = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boards, t.CompositeShapeElements });
            
            CreateTable(
                "WeSketch.ShapeElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(storeType: "ntext"),
                        Type = c.String(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "WeSketch.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 20),
                        RoleInBoard_UserId = c.Int(nullable: false),
                        RoleInBoard_BoardId = c.Int(nullable: false),
                        RoleInBoard_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("WeSketch.UserBoards", "RoleId", c => c.Int(nullable: false));
            DropPrimaryKey("WeSketch.UserBoards");
            DropColumn("WeSketch.UserBoards", "IsFavoriteToUser");
            DropColumn("WeSketch.UserBoards", "Role");
            DropColumn("WeSketch.Boards", "Content");
            AddPrimaryKey("WeSketch.UserBoards", new[] { "UserId", "BoardId", "RoleId" });
            CreateIndex("WeSketch.BoardElements", "ShapeElements");
            CreateIndex("WeSketch.BoardElements", "Boards");
            CreateIndex("WeSketch.UserFavorites", "Users");
            CreateIndex("WeSketch.UserFavorites", "Boards");
            CreateIndex("WeSketch.CompositeBoardElements", "CompositeShapeElements");
            CreateIndex("WeSketch.CompositeBoardElements", "Boards");
            CreateIndex("WeSketch.ShapeElements", "ParentId");
            CreateIndex("WeSketch.Roles", new[] { "RoleInBoard_UserId", "RoleInBoard_BoardId", "RoleInBoard_RoleId" });
            AddForeignKey("WeSketch.BoardElements", "ShapeElements", "WeSketch.ShapeElements", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.BoardElements", "Boards", "WeSketch.Boards", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.UserFavorites", "Users", "WeSketch.Users", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.UserFavorites", "Boards", "WeSketch.Boards", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.CompositeBoardElements", "CompositeShapeElements", "WeSketch.CompositeShapeElements", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.CompositeBoardElements", "Boards", "WeSketch.Boards", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.ShapeElements", "ParentId", "WeSketch.CompositeShapeElements", "Id", cascadeDelete: true);
            AddForeignKey("WeSketch.Roles", new[] { "RoleInBoard_UserId", "RoleInBoard_BoardId", "RoleInBoard_RoleId" }, "WeSketch.UserBoards", new[] { "UserId", "BoardId", "RoleId" });
        }
    }
}
