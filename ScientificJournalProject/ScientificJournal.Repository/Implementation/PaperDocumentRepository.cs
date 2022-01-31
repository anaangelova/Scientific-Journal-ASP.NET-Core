using Microsoft.EntityFrameworkCore;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificJournal.Repository.Implementation
{
    public class PaperDocumentRepository : IPaperDocumentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<PaperDocument> dbSet;

        public PaperDocumentRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<PaperDocument>();
        }

        public void Add(PaperDocument item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(item);
            context.SaveChanges();
        }

        public PaperDocument GetDocumentById(Guid? id)
        {
            return dbSet
                .Where(pd => pd.Id.Equals(id)).FirstOrDefault();
        }
    }
}
