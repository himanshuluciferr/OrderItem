using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuItemListing
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Free Delivery")]
        public bool freeDelivery { get; set; }

        [Required]
        public double Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime dateOfLaunch { get; set; }
        public bool Active { get; set; }

    }
}
