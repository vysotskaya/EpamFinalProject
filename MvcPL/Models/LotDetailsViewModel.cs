﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcPL.Attributes;

namespace MvcPL.Models
{
    public class LotDetailsViewModel : LotCreateViewModel
    {
        public LotDetailsViewModel()
        {
            Bids = new HashSet<BidViewModel>();
        }

        public int CategoryRefId { get; set; }
        public int SellerRefId { get; set; }
        public string SellerEmail { get; set; }
        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; }
        public bool IsConfirm { get; set; }

        [Display(Name = "Current bid")]
        public double CurrentBid { get; set; }

        [Display(Name = "Make bid")]
        [BidValidation(ErrorMessage = "Bid must be more than current bid.")]
        public double MakeBid { get; set; }

        public IEnumerable<BidViewModel> Bids { get; set; } 
    }
}