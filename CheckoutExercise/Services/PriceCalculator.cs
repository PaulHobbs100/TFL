using System.Collections.Generic;
using CheckoutExercise.Models;

namespace CheckoutExercise.Services
{
    /// <summary>
    ///     The code provides a service to calculat a total cost given a list of <see cref="BasketEntry"/>.
    ///     This is one of the classes which is being tested as part of the excercise. The other is <see cref="CheckoutService"/>
    /// </summary>
    public class PriceCalculator
    {
        public int CalculateBasketPrice(List<BasketEntry> basketItems)
        {
            int totalCost = 0;
            foreach (var item in basketItems)
            {
                //totalCost += item.Quantity;     //TODO: this looks wrong 

                //Revised line below, assumes no unique handling of stock items, otherwise further refactoring
                totalCost += item.Quantity * item.UnitPrice;
            }

            return totalCost;
        }
    }
}