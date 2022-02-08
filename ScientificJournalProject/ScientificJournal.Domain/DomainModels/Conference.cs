using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DomainModels
{
    public class Conference
    {
        public Guid Id { get; set; }
        public string ConferenceName { get; set; }
        public string ConferenceImage { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public virtual List<Paper> Papers { get; set; }
    }
}
