using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVC5Course.Models.ViewModel
{
    public class ClientBatchViewModel
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "阿是不會打名字喔")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}