using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicBookShop.DomainModels
{
    public class Category
    {

        [Key]
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
