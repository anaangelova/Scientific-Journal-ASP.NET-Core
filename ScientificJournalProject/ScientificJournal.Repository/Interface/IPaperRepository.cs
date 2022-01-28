using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Repository.Interface
{
    public interface IPaperRepository
    {
        IEnumerable<Paper> GetAll();
        Paper Get(Guid? id);
        void Insert(Paper entity);
        void Update(Paper entity);
        void Delete(Paper entity);
    }
}
