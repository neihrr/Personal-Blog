using System;

namespace BlogMVC.Models
{
    //Holds the attributes of the post objects(a post has to have a title, body, and the time of creation)


    public class ErrorViewModel
    {
        

        public string Title { get; set; }

        public string Body { get; set; }

        public string Genre { get; set; }


        public DateTime Created { get; set; }
         public string RequestId { get; set; }

         public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
