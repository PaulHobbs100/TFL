using System.Collections.Generic;

namespace CheckoutExercise.Models
{
    public class Order
    {
        public List<BasketEntry> Basket { get; set; }
    }
}