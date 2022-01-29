using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DomainModels
{
    public class PaperDocument
    {
        public Guid Id { get; set; }
        public string DocumentName { get; set; }
        public virtual Paper Paper { get; set; }
    }
}
