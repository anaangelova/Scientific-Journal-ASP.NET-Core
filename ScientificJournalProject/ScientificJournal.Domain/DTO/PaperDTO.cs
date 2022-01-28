using ScientificJournal.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DTO
{
    public class PaperDTO
    {
        public Paper Paper { get; set; }
        public String Keywords { get; set; }
        public String AuthorFirst { get; set; }
        public String AuthorSecond { get; set; }
        public String AuthorThird { get; set; }
    }
}
