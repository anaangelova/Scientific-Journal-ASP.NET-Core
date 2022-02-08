using Microsoft.EntityFrameworkCore;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScientificJournal.Repository.Implementation
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Conference> dbSet;

        public ConferenceRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Conference>();
        }

        public List<Conference> GetAllConferences()
        {
            return dbSet.AsEnumerable().ToList();
        }

        public Conference GetConferenceById(Guid? id)
        {
            
            Conference c= dbSet.Where(c => c.Id.Equals(id))
                .Include(c => c.Papers)
                .FirstOrDefault();
            c.Papers = c.Papers.Where(p => p.status.Equals(Status.APPROVED)).ToList();
            return c;
        }

        public Conference GetConferenceByName(string name)
        {
            return dbSet.Where(c => c.ConferenceName.Equals(name))
                .Include(c => c.Papers)
                .FirstOrDefault();
        }

        public List<Paper> GetPapersForConference(string name)
        {
            Conference conference = GetConferenceByName(name);
            List<Paper> all= conference.Papers.Where(p => p.status.Equals(Status.APPROVED)).ToList();
            return all;

        }

        public void Insert(Conference entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }
    }
}
