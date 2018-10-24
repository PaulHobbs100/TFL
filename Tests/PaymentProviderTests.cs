using System.Collections.Generic;
using CheckoutExercise;
using CheckoutExercise.Models;
using CheckoutExercise.Services;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace Tests
{
    /// <summary>
    ///     The test below passes, passes, but there are defects in the code. The current unit test is not a very good test.
    ///     You may wish to refactor it and add extra tests, or scrap it entirely and start from scratch, it’s up to you.
    ///     The following NuGet packages are installed:
    ///     - <see cref="NUnit"/>
    ///     - <see cref="FluentAssertions"/>
    ///     - <see cref="Moq"/>
    ///     You may wish to use additional or alternative testing packages, it's up to you.
    /// </summary>
    /// 
    [TestFixture]
    public class PaymentProviderTests
    {
       
        // My tests start here
        [Test]
        //Test the available balance is not exceeded
        public void ValidOrderWithinBalance()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order1 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber =1, Quantity = 2, UnitPrice = 1}
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order1Result = paymentProvider.PlaceOrder(customer, order1);
            order1Result.Should().Be(true,"Exceeded available customer balance");
        }

        [Test]
        // Test when balance has been exceeded
        public void InvalidOrderExceedsBalance()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order2 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1,Quantity = 10, UnitPrice = 2},
                    new BasketEntry {StockReferenceNumber=2,Quantity = 2, UnitPrice = 5}
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order2Result = paymentProvider.PlaceOrder(customer, order2);
            order2Result.Should().Be(false,"Failed to trap Exceeded available balance");
        }

        [Test]
        //Test for empty basket
        public void BasketEmpty()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order3 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1,Quantity = 0, UnitPrice = 2},
                  
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order3Result = paymentProvider.PlaceOrder(customer, order3);
            order3Result.Should().Be(true, "Basket empty");
        }
        [Test]
        // Test and exclude zero quantities, assumes partial order/backorders are allowed
        public void BasketContainsZeroQuantity_partialOrder()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order4 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1, Quantity = 0, UnitPrice = 2},
                    new BasketEntry {StockReferenceNumber=2, Quantity = 2, UnitPrice = 5}
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order4Result = paymentProvider.PlaceOrder(customer, order4);
            order4Result.Should().Be(true, "Satisfy partial order");
        }
        [Test]
        // Test and allow zero unitprice items, could be a promotion
        public void BasketContainsZeroUnitPprice_partialOrder()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order5 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1, Quantity = 3, UnitPrice = 0},
                    new BasketEntry {StockReferenceNumber=2, Quantity = 2, UnitPrice = 5}
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order5Result = paymentProvider.PlaceOrder(customer, order5);
            order5Result.Should().Be(true, "No zero UnitPrice items found in basket");
        }


        [Test]
        //If we are allowing returned items need to allow negative quantities
        public void CustomerReturnsItems_negativeQuantity()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order6 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1, Quantity = 3, UnitPrice = 2},
                    new BasketEntry {StockReferenceNumber=2, Quantity = -2, UnitPrice = 5}
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order6Result = paymentProvider.PlaceOrder(customer, order6);
            order6Result.Should().Be(true, "Negative quantity/returned item should be allowed");
        }

        [Test]
        //If we are allowing refunded items need to allow negative UnitPrices
        public void CustomerRefundsItems_negativeUnitPrices()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order7 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1, Quantity = 3, UnitPrice = -3},
                    new BasketEntry {StockReferenceNumber=2, Quantity = 2, UnitPrice = 5}
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order7Result = paymentProvider.PlaceOrder(customer, order7);
            order7Result.Should().Be(true, "No zero UnitPrice items found in basket");
        }

        [Test]
        //Test can only order if Stock Item exists, assume partial orders allowed
        //can invalid item(s) be removed successfully
        public void ValidStockItemExists()
        {
            int[] stockitemlist = new int[] { 1,2,3,4};

            var customer = new CustomerAccount { Balance = 100 };

            var order8 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1, Quantity = 3, UnitPrice = 0},
                    new BasketEntry {StockReferenceNumber=99, Quantity = 2, UnitPrice = 3}, //bad stock item
                    new BasketEntry {StockReferenceNumber=2, Quantity = 2, UnitPrice = 5}
                }
            };

            
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order8Result = paymentProvider.ValidateStockItemsInBasket(customer, order8);
            order8Result.Should().Be(true, "Invalid Stock Item, exclude from order");
        }

        [Test]
        //Test can only order if Stock Item exists and in sufficient quantity
        public void InsufficientStockQuantityAvailable()
        {
            var customer = new CustomerAccount { Balance = 10 };

            var order9 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1, Quantity = 3, UnitPrice = 0},
                    new BasketEntry {StockReferenceNumber=2, Quantity = 2, UnitPrice = 5}
                }
            };
            var paymentProvider = new CheckoutService(new NotificationProvider());
            var order9Result = paymentProvider.ValidateStockItemsQuantity(customer, order9);
            order9Result.Should().Be(true, "Invalid Stock quantity, exclude from order");
        }

        [Test]
        public void CheckTotalBalanceAsExpected()
        {
            var order10 = new Order
            {
                Basket = new List<BasketEntry>
                {
                    new BasketEntry {StockReferenceNumber=1, Quantity = 3, UnitPrice = 1},
                    new BasketEntry {StockReferenceNumber=2, Quantity = 2, UnitPrice = 5}
                }
            };
            var validateBalance = new PriceCalculator();
            var order10Result = validateBalance.CalculateBasketPrice( order10.Basket);
            order10Result.Should().Be(13,"Incorrect Balance calculated for basketitems");
        }

    }
}