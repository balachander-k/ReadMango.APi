﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadMango.APi.Models
{
    public class OrderHeader
    {
        [Key]
        public int OrderHeaderId { get; set; }
        [Required]
        public string PickupName { get; set; }
        [Required]
        public string PickupPhoneNumber { get; set; }
        [Required]
        public string PickupEmail { get; set; }
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public Applicationuser User { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public string StripePaymentIntentId { get; set; }   

        public string Status { get; set; }

        public int TotalItems { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
