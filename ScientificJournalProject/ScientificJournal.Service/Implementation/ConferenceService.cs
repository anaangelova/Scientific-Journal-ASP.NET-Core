using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Repository.Implementation;
using ScientificJournal.Repository.Interface;
using ScientificJournal.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Service.Implementation
{

    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository conferenceRepository;

        public ConferenceService(IConferenceRepository conferenceRepository)
        {
            this.conferenceRepository = conferenceRepository;
        }

        public void AddConference(Conference conference)
        {
            conferenceRepository.Insert(conference);
        }



        public List<Conference> GetConferences()
        {
            return conferenceRepository.GetAllConferences();
        }

        public Conference GetDetailsForConference(Guid? id)
        {
            return conferenceRepository.GetConferenceById(id);
        }

        public Conference GetDetailsForConferenceByName(string name)
        {
            return conferenceRepository.GetConferenceByName(name);
        }
    }
}
