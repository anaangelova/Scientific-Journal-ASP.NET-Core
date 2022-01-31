using Microsoft.AspNetCore.Http;
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
        void CreateNewPaper(PaperDTO p, IFormFile file);
        void UpdateExistingPaper(PaperDTO p);
        void DeletePaper(Guid id);
        List<Paper> GetPapersForUser(string userId);
        PaperDTO GetDetailsForEdit(Guid? id);

        List<Paper> GetAllPendingPapers();
        void ApprovePaper(Guid? id);
        void DenyPaper(Guid? id);
    }
}
