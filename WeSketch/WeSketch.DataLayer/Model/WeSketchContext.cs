using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeSketch.DataLayer.Model
{
    public class WeSketchContext : DbContext
    {
        public WeSketchContext() : base("Data Source=160.99.38.140,14330;Initial Catalog=DB_WeSketch;Persist Security Info=True;User ID=stefan81888;Password=sifra1234;")
        {

        }

        #region DbSets
        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("WeSketch");

            #region PrimaryKeys
            modelBuilder.Entity<Board>().HasKey<int>(s => s.Id);
            modelBuilder.Entity<User>().HasKey<int>(s => s.Id);
            modelBuilder.Entity<UserBoards>().HasKey(s => new {s.UserId, s.BoardId});
            #region Auto increment             
            modelBuilder.Entity<Board>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            #endregion  
            #endregion
            #region Columns
            modelBuilder.Entity<Board>()
                        .Property(s => s.Title)
                        .HasMaxLength(20)
                        .IsRequired();
            modelBuilder.Entity<Board>()
                        .Property(s => s.Desription)
                        .HasMaxLength(50)
                        .IsOptional();
            modelBuilder.Entity<Board>()
                        .Property(s => s.DateCreated)
                        .IsOptional();
            modelBuilder.Entity<Board>()
                        .Property(s => s.ActiveBoard)
                        .IsOptional();
            modelBuilder.Entity<Board>()
                        .Property(s => s.Password)
                        .IsOptional();
            modelBuilder.Entity<Board>()
                        .Property(s => s.Content)
                        .HasColumnType("ntext");


            modelBuilder.Entity<UserBoards>()
                        .Property(s => s.Role)
                        .HasMaxLength(1000)
                        .IsRequired();

            modelBuilder.Entity<UserBoards>()
                        .Property(s => s.IsFavoriteToUser)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(s => s.Username)
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("Username") { IsUnique = true }));          
            modelBuilder.Entity<User>()
                        .Property(s => s.Email)
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("Email") { IsUnique = true }));
            modelBuilder.Entity<User>()
                        .Property(s => s.Password)                       
                        .IsRequired();
            modelBuilder.Entity<User>()
                        .Property(s => s.LastName)
                        .IsOptional();
            modelBuilder.Entity<User>()
                        .Property(s => s.FirstName)
                        .IsOptional();
            modelBuilder.Entity<User>()
                        .Property(s => s.DateRegistered)
                        .IsOptional();
            modelBuilder.Entity<User>()
                        .Property(s => s.DateOfBirth)
                        .IsOptional();
            modelBuilder.Entity<User>()
                        .Property(s => s.ActiveAccount)
                        .IsOptional();
                        #endregion
            #region Foreign Keys        
            modelBuilder.Entity<UserBoards>()
                        .HasRequired(s => s.User)
                        .WithMany(s => s.UserBoards)
                        .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<UserBoards>()
                        .HasRequired(s => s.Board)
                        .WithMany(s => s.UserBoards)
                        .HasForeignKey(usr => usr.BoardId);           
            #endregion
        }
    }
}
