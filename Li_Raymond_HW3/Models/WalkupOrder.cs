using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Li_Raymond_HW3.Models
{
    public class WalkupOrder: Order 
    {
        const Decimal SALES_TAX_RATE = 0.085M;
        [Display(Name = "Customer Name:")] 
        public String? CustomerName { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal SalesTax { get; set; }
        public void CalcTotals()
        {
            base.CalcSubtotals();
            SalesTax = base.Subtotal * SALES_TAX_RATE;
            base.Total = SalesTax + base.Subtotal;
        }
    }
}
