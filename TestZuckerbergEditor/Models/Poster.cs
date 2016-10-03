using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestZuckerbergEditor.Models
{    
    public class Poster
    {
        [Key]
        public virtual int id { get; set; }
        public virtual string posterPageNr { get; set; }
        public virtual string imageName { get; set; }
        public virtual string question { get; set; }
        public virtual string title { get; set; }
        public virtual string nextPageLink { get; set; }
        public virtual List<Comment> comments { get; set; }        
    }        
}


