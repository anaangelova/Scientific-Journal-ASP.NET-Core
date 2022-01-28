using Microsoft.EntityFrameworkCore;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.Identity;
using ScientificJournal.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificJournal.Repository.Implementation
{
    public class PapersUsersRepository : IPapersUsersRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<PapersUsers> dbSet;

        public PapersUsersRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<PapersUsers>();
        }

        public void Add(PapersUsers item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(item);
            context.SaveChanges();
        }

        public List<ScienceUser> GetAuthorsForPaper(Guid? id)
        {
            return dbSet
                .Where(pu => pu.PaperId.Equals(id))
                .Include(pu => pu.ScienceUser)
                .Select(pu => pu.ScienceUser).ToList();
        }
    }
}
