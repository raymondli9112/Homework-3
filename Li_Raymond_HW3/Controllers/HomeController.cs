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
            return View();
        }
        public ViewResult CateringTotals(CateringOrder cateringOrder)
        {
            TryValidateModel(cateringOrder);
            if (ModelState.IsValid == false)
            {
                return View("CheckoutCatering", cateringOrder);
            }
            cateringOrder.CustomerType = CustomerType.Catering;
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
    }
}