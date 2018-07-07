using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModel
{
    public class ProductViewModel
    {

        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal? Price { get; set; }
        [Required]
        public decimal? Stock { get; set; }
    }
}