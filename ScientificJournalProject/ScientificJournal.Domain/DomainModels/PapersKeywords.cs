using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DomainModels
{
    public class PapersKeywords
    {
        public Guid PaperId { get; set; }
        public Paper Paper { get; set; }
        public string Keyword { get; set; }
    }
}
