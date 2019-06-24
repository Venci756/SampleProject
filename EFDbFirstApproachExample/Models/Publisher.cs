using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFDbFirstApproachExample.Models
{
    public class Publisher
    {
        [Key]
        public long PublisherID { get; set; }
        public string PublisherName { get; set; }
        
    }
}