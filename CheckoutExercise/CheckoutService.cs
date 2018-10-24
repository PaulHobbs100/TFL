using System.Linq;
using CheckoutExercise.Models;
using CheckoutExercise.Services;

namespace CheckoutExercise
{
    /// <summary>
    ///     The code represents is a simple checkout process through which customer can pay for items from a pre-paid balance.
    ///     This is one of the classes which is being tested as part of the excercise. The other is <see cref="PriceCalculator"/>
    /// </summary>
    public class CheckoutService
    {
        private readonly NotificationProvider _emailProvider;
        private readonly PriceCalculator _priceCalculator;

        public CheckoutService(NotificationProvider emailProvider)
        {
            _emailProvider = emailProvider;
            _priceCalculator = new PriceCalculator();
        }

        public bool PlaceOrder(CustomerAccount customer, Order order)
        {
            var totalCost = _priceCalculator.CalculateBasketPrice(order.Basket);

            if (totalCost > customer.Balance)
                return false;

            //TODO: consider own method, placeholder has 2 responsibilities
            _emailProvider.SendOrderNotification(totalCost, order.Basket.Sum(bi => bi.Quantity));

            customer.Balance -= totalCost;
            customer.OrdersPlaced.Add(order);

            return true;
        }

        ////////////////// my additions ////////////////////////////
        
        //Clean basket by removing invalid stock items, assumes partial orders allowed
        public bool ValidateStockItemsInBasket(CustomerAccount customer, Order order)
        {
            //TODO: storage of valid stock item values
            // use foreach over order object to isolate and remove invalid stockitems
            return false;
        }

        public bool ValidateStockItemsQuantity(CustomerAccount customer, Order order)
        {
            //TODO:for each stock item ensure stock item quantity is avalable
            return false;
        }
    }
    
}