using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Repository.Interface
{
    public interface IPaperDocumentRepository
    {
        void Add(PaperDocument item);
    }
}
