using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Data
{
    public class Context : DbContext
    {
        public Context() : base("Musicalog")
        {
            Database.SetInitializer<Context>(new MusicalogDBInitialiser());
        }

        public DbSet<Model.Album> Albums { get; set; }

        public DbSet<Model.Artist> Artists { get; set; }

        public DbSet<Model.MediaType> MediaTypes { get; set; }

        public DbSet<Model.RecordLabel> RecordLabels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.Album>().ToTable("Album")
                .HasKey(a => a.Id)
                .Property(a => a.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Model.Album>().Property(a => a.Name).HasMaxLength(100).IsUnicode(false).IsRequired();
            
            modelBuilder.Entity<Model.Album>().HasRequired(a => a.Artist).WithMany(art => art.Albums).HasForeignKey(a => a.ArtistId);
            modelBuilder.Entity<Model.Album>().HasRequired(a => a.Label).WithMany(lab => lab.Albums).HasForeignKey(a => a.RecordLabelId);
            modelBuilder.Entity<Model.Album>()
                .HasMany(a => a.MediaTypes)
                .WithMany(mt => mt.Albums)
                .Map(amt =>
                {
                    amt.MapLeftKey("AlbumId");
                    amt.MapRightKey("MediaTypeId");
                    amt.ToTable("AlbumMediaType");
                });

            modelBuilder.Entity<Model.Artist>().ToTable("Artist")
                .HasKey(a => a.Id)
                    .Property(a => a.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Model.Artist>().Property(a => a.Name).HasMaxLength(100).IsUnicode(false).IsRequired();

            modelBuilder.Entity<Model.RecordLabel>().ToTable("RecordLabel")
                .HasKey(rl => rl.Id)
                .Property(rl => rl.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Model.RecordLabel>().Property(rl => rl.Name).HasMaxLength(100).IsUnicode(false).IsRequired();

            modelBuilder.Entity<Model.MediaType>().ToTable("MediaType")
                .HasKey(mt => mt.Id)
                .Property(mt => mt.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Model.MediaType>().Property(mt => mt.Name).HasMaxLength(100).IsUnicode(false).IsRequired();
        }
    }
}
