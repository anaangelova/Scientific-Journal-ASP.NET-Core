using System;
using System.Collections.Generic;
using System.Text;

namespace ScientificJournal.Domain.DomainModels
{
    public class Conference
    {
        public Guid Id { get; set; }
        public string ConferenceName { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Paper> Papers { get; set; }
    }
}
