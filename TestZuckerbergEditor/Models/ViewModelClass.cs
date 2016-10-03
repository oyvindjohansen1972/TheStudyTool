using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestZuckerbergEditor.Models
{
    public class ViewModelClass
    {
        public string uniqueIdentifier { get; set; }
        public string title { get; set; }
        public string imageName { get; set; }
        public string question { get; set; }
        public string nextPageLink { get; set; }
        public string inputCommentFromUser { get; set; }
        public List<string> comments { get; set; }      
    }
    
    public class ViewModelForHomeView
    {
        public string description { get; set; }
        public string posterIdentifier { get; set; }        
    }

    public class InputComment
    {
        public string comment { get; set; }
        public string posterPageNr { get; set; }
    }
}