using System;
using System.ComponentModel.DataAnnotations;

namespace ComicBookShop.DomainModels
{
    public class Publisher
    {
        [Key]
        public long PublisherID { get; set; }
        public string PublisherName { get; set; }

    }
}
