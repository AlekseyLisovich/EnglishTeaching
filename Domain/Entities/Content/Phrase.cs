using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Entities.Content
{
    public class Phrase : BaseEntity
    { 
        public string Sentence { get; set; }
        public string Description { get; set; }
        public string Translation { get; set; }
    }
}
