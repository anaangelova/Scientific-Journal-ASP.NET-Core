using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Service.Interface
{
    public interface IPaperService
    {
        List<Paper> GetAllPapers();
        PaperDetailsDTO GetDetailsForPaper(Guid? id);
        void CreateNewPaper(PaperDTO p);
        void UpdateExistingPaper(Paper p);
        void DeletePaper(Guid id);
    }
}
