using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Repository.Interface
{
    public interface IConferenceRepository
    {
        List<Conference> GetAllConferences();
        Conference GetConferenceByName(string name);
        Conference GetConferenceById(Guid? id);
        List<Paper> GetPapersForConference(string name);
        void Insert(Conference entity);

    }
}
