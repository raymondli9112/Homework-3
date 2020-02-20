using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Li_Raymond_HW3.Models
{   
    public enum CustomerType
    {
        Walkup, 
        Catering
    }
    public abstract class Order
    {
        const Decimal TACO_PRICE = 3; //Fields (Named Constants) 
        const Int32 MAX_ITEMS = 5000; //maximum number of tacos or sandwiches allowed
        const Decimal SANDWICH_PRICE = 5;
        [Display(Name = "Customer Type:")]
        public CustomerType CustomerType { get; set; }
        [Range(minimum:0, maximum: MAX_ITEMS)]
        [RegularExpression("([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-4][0-9]{3}|5000)", ErrorMessage = "Number of sandwiches must be between 0 and 5000!")]
        [Display(Name = "Number of Sandwiches:")]
        public Int32 NumberOfSandwiches { get; set; }
        [Range(minimum:0, maximum: MAX_ITEMS)]
        [RegularExpression("([0-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-4][0-9]{3}|5000)", ErrorMessage = "Number of tacos must be between 0 and 5000!")]
        [Display(Name = "Number of Tacos:")]      
        public Int32 NumberOfTacos { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Sandwich Subtotal:")]
        public Decimal SandwichSubtotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Subtotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Taco Subtotal:")]
        public Decimal TacoSubtotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Total { get; set; }
        [Display(Name = "Total Items:")]
        public Int32 TotalItems { get; set; }
        
        public void CalcSubtotals()
        {
            TotalItems = NumberOfSandwiches + NumberOfTacos;
            SandwichSubtotal = NumberOfSandwiches * SANDWICH_PRICE;
            TacoSubtotal = NumberOfTacos * TACO_PRICE;
            Subtotal = SandwichSubtotal + TacoSubtotal;            
            return;
        }
    }
}
