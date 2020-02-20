using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Li_Raymond_HW3.Models
{
    public class CateringOrder: Order
    {
        [Required(ErrorMessage = "Customer Code is Required")]
        [RegularExpression("([A-Za-z]){2,4}", ErrorMessage = "Customer code must be 2-4 characters.")] 
        [Display(Name= "Customer Code:")]
        public String CustomerCode { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name= "Delivery Fee:")]
        public Decimal DeliveryFee { get; set; }
        public Boolean PreferredCustomer { get; set; }
        public void CalcTotals(Decimal decDeliveryFee)
        {
            base.CalcSubtotals();
            DeliveryFee = PreferredCustomer ? 0 : decDeliveryFee;
            base.Total = Subtotal + DeliveryFee;
            return;
        }
    }
}
