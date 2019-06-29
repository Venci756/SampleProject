using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicBookShop.DomainModels
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Authors { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        [Display(Name = "Date of purchase")]
        public Nullable<System.DateTime> DateOfPurpose { get; set; }
        [Display(Name = "Availability Status")]
        [Required(ErrorMessage = "Please select the availability status.")]
        public string AvailabilityStatus { get; set; }
        public long CategoryID { get; set; }
        public long PublisherID { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }


        public virtual Publisher Publisher { get; set; }
        public virtual Category Category { get; set; }

    }
}
