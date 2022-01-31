using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DomainModels
{
    public enum Status
    {
        PENDING,
        APPROVED,
        DENIED
    }
    public class Paper
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AreaOfResearch { get; set; }
        public string Abstract { get; set; }
        public virtual ICollection<PapersUsers> AuthorsForPaper { get; set; }
        public virtual ICollection<PapersKeywords> Keywords { get; set; }
        public PaperDocument PaperDocument { get; set; }
        public Guid PaperDocumentId { get; set; }
        public Status status { get; set; }
        public Guid ConferenceId { get; set; }
        public Conference Conference { get; set; }




    }
}
