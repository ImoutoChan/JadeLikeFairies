using System;
using System.IO;
using System.Linq;
using JadeLikeFairies.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JadeLikeFairies.Data
{
    public class FairiesContext : DbContext
    {
        public FairiesContext(DbContextOptions<FairiesContext> options) : base(options)
        {
        }

        public DbSet<Novel> Novels { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<NovelGenre> NovelGenres { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<NovelTag> NovelTags { get; set; }

        public DbSet<NovelType> Types { get; set; }

        /// <summary>
        /// Autofill CreatedDate and UpdatedDate on creation/update
        /// </summary>
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added 
                            || x.State == EntityState.Modified);

            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (var entry in modifiedEntries)
            {
                var entity = (EntityBase)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                }
                
                entity.UpdatedDate = now;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildNovel(modelBuilder);
            BuildNovelGenre(modelBuilder);
            BuildNovelTag(modelBuilder);
            BuildTag(modelBuilder);
            BuildGenre(modelBuilder);
            BuildType(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void BuildNovelTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NovelTag>().HasOne(x => x.Novel).WithMany(x => x.Tags).HasForeignKey(p => p.NovelId);
            modelBuilder.Entity<NovelTag>().HasOne(x => x.Tag).WithMany(x => x.Novels).HasForeignKey(p => p.TagId);

            BuildEntityBase(modelBuilder.Entity<NovelTag>());
        }
        
        private static void BuildNovelGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NovelGenre>().HasOne(x => x.Novel).WithMany(x => x.Genres).HasForeignKey(p => p.NovelId);
            modelBuilder.Entity<NovelGenre>().HasOne(x => x.Genre).WithMany(x => x.Novels).HasForeignKey(p => p.GenreId);

            BuildEntityBase(modelBuilder.Entity<NovelGenre>());
        }

        private static void BuildType(ModelBuilder modelBuilder)
        {
            var entityTypeBuilder = modelBuilder.Entity<NovelType>();

            entityTypeBuilder.Property(x => x.Name).IsRequired();
            entityTypeBuilder.HasIndex(x => x.Name);

            BuildEntityBase(entityTypeBuilder);
        }

        private static void BuildGenre(ModelBuilder modelBuilder)
        {
            var entityTypeBuilder = modelBuilder.Entity<Genre>();

            entityTypeBuilder.Property(x => x.Name).IsRequired();
            entityTypeBuilder.HasIndex(x => x.Name);

            BuildEntityBase(entityTypeBuilder);
        }

        private static void BuildTag(ModelBuilder modelBuilder)
        {
            var entityTypeBuilder = modelBuilder.Entity<Tag>();

            entityTypeBuilder.Property(x => x.Name).IsRequired();
            entityTypeBuilder.HasIndex(x => x.Name);

            BuildEntityBase(entityTypeBuilder);
        }

        private static void BuildNovel(ModelBuilder modelBuilder)
        {
            var entityTypeBuilder = modelBuilder.Entity<Novel>();

            entityTypeBuilder.HasIndex(x => x.UpdatedDate);
            entityTypeBuilder.HasIndex(x => x.CreatedDate);
            entityTypeBuilder.HasIndex(x => new { x.Title, x.AltTitles });

            entityTypeBuilder.HasOne(x => x.Type).WithMany(x => x.Novels).HasForeignKey(p => p.TypeId);

            BuildEntityBase(entityTypeBuilder);
        }

        private static void BuildEntityBase<T>(EntityTypeBuilder<T> builder)
            where T : EntityBase
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired();
        }
    }
}