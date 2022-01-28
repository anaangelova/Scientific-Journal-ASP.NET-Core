using ScientificJournal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ScienceUser> GetAll();
        ScienceUser Get(string id);
        void Insert(ScienceUser entity);
        void Update(ScienceUser entity);
        void Delete(ScienceUser entity);
        ScienceUser GetByEmail(string email);
    }
}
