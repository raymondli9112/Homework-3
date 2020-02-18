//Raymond Li
//Date: 2/12/2020
//Homework 3
//Calculated total price of food truck order depending on number of tacos and sandwiches ordered
using Li_Raymond_HW3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
namespace Li_Raymond_HW3.Controllers
{
    public class HomeController : Controller
    {
        //declare constant variables for tacos, sandwiches, and tax
        const decimal tacoPrice = 3;
        const decimal sandwichPrice = 5;
        const decimal tax = 0.085M;

        public ViewResult Index()
        {
            return View();
        }
        public ViewResult CheckoutCatering()
        {
            CateringOrder cateringOrder = new CateringOrder();
            cateringOrder.CustomerType = CustomerType.Catering;
            return View(cateringOrder);
        }
        [HttpPost]
        public ViewResult CateringTotals(CateringOrder cateringOrder)       
        {
            TryValidateModel(cateringOrder);
            if (ModelState.IsValid == false)    
            {
                return View("CheckoutCatering", cateringOrder);
            }
            cateringOrder.CalcTotals(cateringOrder.DeliveryFee);
            return View("CateringTotals", cateringOrder);
        }        
        public ViewResult CheckoutWalkup()
        {
            return View();
        }
        public ViewResult WalkupTotals(WalkupOrder walkupOrder)
        {
            TryValidateModel(walkupOrder);
            if (ModelState.IsValid == false)
            {
                return View("CheckoutWalkup", walkupOrder);
            }
            walkupOrder.CalcTotals();
            return View("WalkupTotals", walkupOrder);
        }
        public ViewResult SubmitOrder(string strCustomerCode, string strNumberOfTacos, string strNumberOfSandwiches)
        {
            //call check customer code method to see if customer code passes validation
            Boolean bolCheckCustomerCode = ValidateCustomerCode(strCustomerCode);
            if (bolCheckCustomerCode == false)
            {
                ViewBag.ErrorMessage = "Customer code must be 2-4 characters.";
                return View("Index");
            }

            int intTacos;
            int intSandwiches;
            try
            {
                // call validate items method to validate number of sandwiches and tacos
                intTacos = ValidateItems(strNumberOfTacos);
                intSandwiches = ValidateItems(strNumberOfSandwiches);
                if (intTacos + intSandwiches == 0)
                {
                    throw new Exception("You must purchase at least one item!");
                }
                if (String.IsNullOrEmpty(strNumberOfSandwiches)&&(String.IsNullOrEmpty(strNumberOfTacos)))
                {
                    throw new Exception("You must purchase at least one item!");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }            
            //calculate outputs
            String UpperCustomerCode = strCustomerCode.ToUpper();
            int intTotalItems = intTacos + intSandwiches;
            Decimal TacoSubtotal = intTacos * tacoPrice;
            Decimal SandSubtotal = intSandwiches * sandwichPrice;
            Decimal PretaxSubtotal = TacoSubtotal + SandSubtotal;
            Decimal TempTax = PretaxSubtotal * tax;
            Decimal Grandtotal = PretaxSubtotal + TempTax;

            //use viewbag to display in the view
            ViewBag.CustomerCode = UpperCustomerCode;
            ViewBag.TotalItems = intTotalItems.ToString(); 
            ViewBag.TacoSubtotal = TacoSubtotal.ToString("C2");
            ViewBag.SandSubtotal = SandSubtotal.ToString("C2");
            ViewBag.PretaxSubtotal = PretaxSubtotal.ToString("C2");
            ViewBag.Tax = TempTax.ToString("C2");
            ViewBag.Grandtotal = Grandtotal.ToString("C2");

            return View();
        }
        

        //method to check if customer code is valid 
        public Boolean ValidateCustomerCode(string strCustomerCode)
        {
            if (String.IsNullOrEmpty(strCustomerCode)) //check to see customer code is empty 
            {
                return false;
            }
            if (strCustomerCode.Length < 2 || strCustomerCode.Length > 4) //checks length of customer code
            {
                return false;
            }
            if (!(strCustomerCode.All(char.IsLetter))) //checks if all characters in customer code are alphabetical letters
            {
                return false;
            }
            else  //everything is okay, return true
            {
                return true;
            }
        }

        //method to check if data for number of tacos and number of sandwiches is valid
        public int ValidateItems(string strNumberOfItems)
        {         
            int intNumberOfItems;

            try
            {
                intNumberOfItems = Convert.ToInt32(strNumberOfItems);
            }

            catch (Exception ex)
            {
                throw new Exception(strNumberOfItems + " is not a valid integer!", ex); 
            }

            if (intNumberOfItems % 1 != 0)
            {
                throw new Exception(strNumberOfItems + " is not a valid integer!"); //check if number of tacos or sandwiches is a whole number
            }

            if (intNumberOfItems < 0)
            {
                throw new Exception(strNumberOfItems + " is not in the required range!"); //checks if number of tacos or sandwiches entered is negative
            }

            return intNumberOfItems;
        }           
    }
}