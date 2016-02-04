using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Section name")]
        public string SectionName { get; set; }
        public int SectionRefId { get; set; }

        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
        public int CategoryRefId { get; set; }

        [Display(Name = "Action")]
        public bool ToConfirm { get; set; }

        public bool Result { get; set; }
    }
}