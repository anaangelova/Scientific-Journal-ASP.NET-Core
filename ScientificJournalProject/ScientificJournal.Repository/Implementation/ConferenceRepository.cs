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
            List<Conference> confs= dbSet
                .Where(c => !string.IsNullOrEmpty(c.ConferenceImage))
                .AsEnumerable().ToList();
            return confs;
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
            Conference summary = dbSet.Where(c => c.ConferenceName.Equals(name) && !string.IsNullOrEmpty(c.ConferenceImage))
                .Include(c => c.Papers).FirstOrDefault();


            List<Conference> tmp =  dbSet.Where(c => c.ConferenceName.Equals(name) && string.IsNullOrEmpty(c.ConferenceImage))
                .Include(c => c.Papers)
                .ToList();

            List<Paper> papers = new List<Paper>();
           
            foreach(Conference c in tmp)
            {
                papers.AddRange(c.Papers);
            }
            papers = papers.Distinct<Paper>().ToList();

            foreach(Paper p in papers)
            {
                summary.Papers.Add(p);
            }
            summary.Papers = summary.Papers.Where(p => p.status.Equals(Status.APPROVED)).ToList();
            return summary;
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
