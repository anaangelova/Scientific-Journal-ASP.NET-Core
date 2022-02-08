using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Service.Interface
{
    public interface IConferenceService
    {
        void AddConference(Conference conference);
        List<Conference> GetConferences();
        Conference GetDetailsForConference(Guid? id);
    }
}
