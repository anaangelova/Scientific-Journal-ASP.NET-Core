using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Service.Interface
{
    public interface IPaperDocumentService
    {
        PaperDocument GetPaper(Guid? id);
    }
}
