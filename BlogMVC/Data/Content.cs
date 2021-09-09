using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Data
{
    
    public class Content
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public string Title { get; set; }
        public string Body { get; set; }

        public string Genre { get; set; }


       
        public DateTime Created { get; set; }

        //public string RequestId { get; set; }

    }
}
