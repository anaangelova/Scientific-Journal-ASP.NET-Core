using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Repository.Interface;
using ScientificJournal.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Service.Implementation
{
    public class PaperDocumentService : IPaperDocumentService
    {
        private readonly IPaperDocumentRepository paperDocumentRepository;

        public PaperDocumentService(IPaperDocumentRepository paperDocumentRepository)
        {
            this.paperDocumentRepository = paperDocumentRepository;
        }

        public PaperDocument GetPaper(Guid? id)
        {
            return paperDocumentRepository.GetDocumentById(id);
        }
    }
}
