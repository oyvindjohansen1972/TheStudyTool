using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestZuckerbergEditor.Models
{
    public class Wall
    {
        [Key]
        public virtual int id { get; set; }
        public virtual string uniqueIdentifier { get; set; }
        public virtual string wallOwner { get; set; }
        public virtual string wallName { get; set; }
        public virtual List<Poster> posters { get; set; }
    }
}