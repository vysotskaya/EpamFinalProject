using System;
using System.ComponentModel.DataAnnotations;
using MvcPL.Models;

namespace MvcPL.Attributes
{
    //public class BidValidationAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var currentBid = ((LotDetailsViewModel) validationContext.ObjectInstance).CurrentBid;
    //        double makeBid = (double)value;
    //        if (makeBid > currentBid)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        return new ValidationResult("Incorrect bid.");
    //    }
    //}
}