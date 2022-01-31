using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Repository.Interface
{
    public interface IPapersKeywordsRepository
    {
        void Add(PapersKeywords item);
        List<string> FindKeywordsByPaper(Guid? id);
        void Update(PapersKeywords entity);
        void Delete(PapersKeywords entity);
        void DeleteAllKeywordsForPaper(Guid? id);
    }
}
