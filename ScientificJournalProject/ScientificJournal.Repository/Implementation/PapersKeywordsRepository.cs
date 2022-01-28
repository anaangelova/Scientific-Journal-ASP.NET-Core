using Microsoft.EntityFrameworkCore;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificJournal.Repository.Implementation
{
    public class PapersKeywordsRepository : IPapersKeywordsRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<PapersKeywords> dbSet;

        public PapersKeywordsRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<PapersKeywords>();
        }

        public void Add(PapersKeywords item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(item);
            context.SaveChanges();
        }

        public List<string> FindKeywordsByPaper(Guid? id)
        {
            return dbSet.Where(pk => pk.PaperId.Equals(id))
                .Select(pk => pk.Keyword)
                .ToList();
        }
    }
}
