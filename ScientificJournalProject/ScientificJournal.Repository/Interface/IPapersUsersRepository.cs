using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Repository.Interface
{
    public interface IPapersUsersRepository
    {
        void Add(PapersUsers item);
        List<ScienceUser> GetAuthorsForPaper(Guid? id);
        List<Paper> GetPapersForUser(string userId);
        void Update(PapersUsers entity);
        void Delete(PapersUsers entity);
    }
}
