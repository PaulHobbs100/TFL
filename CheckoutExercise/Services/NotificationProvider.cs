namespace CheckoutExercise.Services
{
    /// <summary>
    ///     This class is a stub of a notification provider for sending messages to the customer.
    ///     It is provided for reference only, you do not need to modify this class as part of the excercise.
    /// </summary>
    public class NotificationProvider
    {
        private const string OrderNotificationTemplate =
            "Thank you for your order of {0} items for £{1}";

        public void SendOrderNotification(int itemCount, int totalCost)
        {
            SendNotification(string.Format(OrderNotificationTemplate, itemCount, totalCost));
        }

        public void SendNotification(string notificationMessage)
        {
            // TODO
            //throw new NotImplementedException();
        }
    }
}