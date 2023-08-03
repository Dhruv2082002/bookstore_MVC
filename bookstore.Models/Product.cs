﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bookstore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]

        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }

        [Range(1,10000)]
        [Required]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name ="Price for Small Order")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50-100")]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        public double Price100 { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
