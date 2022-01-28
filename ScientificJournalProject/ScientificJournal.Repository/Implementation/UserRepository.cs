using Microsoft.EntityFrameworkCore;
using ScientificJournal.Domain.Identity;
using ScientificJournal.Repository.Interface;
using ScientificJournal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificJournal.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<ScienceUser> dbSet;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<ScienceUser>();
        }

        public void Delete(ScienceUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public ScienceUser Get(string id)
        {
            return dbSet
                .Include(u => u.PapersFromUser)
                .ThenInclude(pu => pu.Paper)
                .SingleOrDefault(u => u.Id.Equals(id));
        }

        public IEnumerable<ScienceUser> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public ScienceUser GetByEmail(string email)
        {
            return dbSet.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }

        public void Insert(ScienceUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Update(ScienceUser entity)
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
