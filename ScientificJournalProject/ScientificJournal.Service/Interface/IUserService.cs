using ScientificJournal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Service.Interface
{
    public interface IUserService
    {
        public IEnumerable<ScienceUser> getAllUsers();
        public bool isAdmin(string id);
        public ScienceUser getUser(string id);
    }
}
