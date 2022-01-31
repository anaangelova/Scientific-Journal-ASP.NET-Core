using Microsoft.EntityFrameworkCore;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Repository;
using ScientificJournal.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificJournal.Repository.Implementation
{
    public class PaperRepository : IPaperRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Paper> dbSet;

        public PaperRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Paper>();
        }

        public void Delete(Paper entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Remove(entity);

            context.SaveChanges();
        }

        public Paper Get(Guid? id)
        {
            return dbSet
                .Where(p => p.Id.Equals(id))
                .Include(p => p.AuthorsForPaper)
                .ThenInclude(pa => pa.ScienceUser)
                .Include(p => p.Keywords)
                .Include(p => p.PaperDocument)
                .Include(p => p.Conference)
                .ThenInclude(c => c.Papers)
                .FirstOrDefault();
        }

        public IEnumerable<Paper> GetAll()
        {
            return dbSet
                .Where(p => p.status.Equals(Status.APPROVED))
                .AsEnumerable();
        }

        public void Insert(Paper entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Update(Paper entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            
            dbSet.Update(entity);
            context.SaveChanges();

            
        }

        public List<Paper> AllPapersForUser(string userId)
        {
            return null; //not implemented
        }

        public List<Paper> GetAllPendingPapers()
        {
            return dbSet
                        .Where(p => p.status.Equals(Status.PENDING))
                        .ToList();
        }

        public void ApprovePaper(Paper paper)
        {
            paper.status = Status.APPROVED;
            Update(paper);
        }

        public void DenyPaper(Paper paper)
        {
            paper.status = Status.DENIED;
            Update(paper);
        }
    }
}
