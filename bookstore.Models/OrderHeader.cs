using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.Models
{
    public class OrderHeader
    {
        //id application userid applicationuser payment status order status order date shipping date order total order count payment date payment due date tracking id carrier payment intent id phone number street address city state postal code name 

        public int Id { get; set; } 
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public string? PaymentStatus { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string? TrackingId { get; set; }
        public string? Carrier { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        [Required]

        public string StreetAddress { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string PostalCode { get; set; }

        [Required]
        public string State { get; set; }

       
        [Required]

        public string Name { get; set; }


    }
}
