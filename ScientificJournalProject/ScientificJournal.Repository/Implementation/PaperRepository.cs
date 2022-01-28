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
            return dbSet.SingleOrDefault(s => s.Id.Equals(id));
        }

        public IEnumerable<Paper> GetAll()
        {
            return dbSet.AsEnumerable();
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
    }
}
