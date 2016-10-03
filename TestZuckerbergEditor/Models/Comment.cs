using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestZuckerbergEditor.Models
{
    public class Comment
    {
        [Key]
        public virtual int id { get; set; }
        public virtual string comment { get; set; }       
    }
}