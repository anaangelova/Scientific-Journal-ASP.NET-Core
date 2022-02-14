using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ScienceUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PapersUsers> PapersUsers { get; set; }
        public virtual DbSet<PapersKeywords> PapersKeywords { get; set; }
        public virtual DbSet<Conference> Conferences { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //avtomatsko generiranje na id za sekoja od klasite
           /* builder.Entity<Paper>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();*/

            //definiranje na kompoziten PK za klasata/modelot PapersUsers i za PapersKeywords - slab entitet
            builder.Entity<PapersUsers>().HasKey(z => new { z.PaperId, z.ScienceUserId });
            builder.Entity<PapersKeywords>().HasKey(z => new { z.PaperId, z.Keyword });

            //definiranje na 2 OneToMany relacii i  definiranje na FKs vo 
            builder.Entity<PapersUsers>()
                .HasOne(z => z.Paper)
                .WithMany(p => p.AuthorsForPaper)
                .HasForeignKey(z => z.PaperId);

            builder.Entity<PapersUsers>()
                .HasOne(z => z.ScienceUser)
                .WithMany(s => s.PapersFromUser)
                .HasForeignKey(z => z.ScienceUserId);

            //definiranje OneToMany megju Papers i PapersKeywords
            builder.Entity<PapersKeywords>()
                .HasOne(pk => pk.Paper)
                .WithMany(p => p.Keywords)
                .HasForeignKey(pk => pk.PaperId);

            //definiranje na PK za PaperDocument  dali treba???

            //OneToOne relacija megju Paper i PaperDocument
            builder.Entity<Paper>()
                .HasOne(p => p.PaperDocument)
                .WithOne(pd => pd.Paper)
                .HasForeignKey<Paper>(p => p.PaperDocumentId);
           
            builder.Entity<Conference>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            //OneToMany Conference:Paper
            builder.Entity<Paper>()
                .HasOne(p => p.Conference)
                .WithMany(c => c.Papers)
                .HasForeignKey(p => p.ConferenceId);


        }

    }
}
