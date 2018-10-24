using System.Collections.Generic;

namespace CheckoutExercise.Models
{
    public class CustomerAccount
    {
        public int Balance;
        public List<Order> OrdersPlaced = new List<Order>();
    }
}