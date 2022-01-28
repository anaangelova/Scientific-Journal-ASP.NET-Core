using ScientificJournal.Domain.Identity;
using ScientificJournal.Repository.Interface;
using ScientificJournal.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<ScienceUser> getAllUsers()
        {
            return userRepository.GetAll();
        }

        public ScienceUser getUser(string id)
        {
            return userRepository.Get(id);
        }

        public bool isAdmin(string id)
        {
            ScienceUser scienceUser = this.getUser(id);
            return scienceUser.isAdmin;
        }
    }
}
