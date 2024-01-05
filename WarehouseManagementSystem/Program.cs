using System.Net.Mail;
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
using static WarehouseManagementSystem.Business.OrderProcessor;

namespace WarehouseManagementSystem
{
    public class Program
    {

        public static bool SendMessageToWareHouse(Order order)
        {
            Console.WriteLine($"Please pack the order {order.GetType().Name}");
            return true;
        }

        public static void SendConfirmationEmail(Order order)
        {
            Console.WriteLine($"Order Confirmation Email {order.GetType().Name}");
        }

        public static void Main(string[] args)
        {
            var oder = new Order()
            {
                LineItems = new[]
                {
                    new Item { Name = "PS1", Price = 50},
                    new Item { Name = "PS2", Price = 60},
                    new Item { Name = "PS4", Price = 70},
                    new Item { Name = "PS5", Price = 80},
                }
            };

            ///multicast Delegates and Chain
            //OrderInitialized chain2 = SendMessageToWareHouse;
            //chain += SendMessageToManager;
            //chain += SendMessageToBoss;

            ///otherway to multicast delegates and chain
            //OrderInitialized chain2 = (OrderInitialized)SendMessageToWareHouse + SendMessageToManager + SendMessageToBoss;

            ///Anornymous function
            //OrderInitialized chain2 = (order) =>
            //{
            //    SendMessageToWareHouse(order);
            //    SendMessageToManager(order);
            //    SendMessageToBoss(order);

            //    return true;
            //};

            //var process = new OrderProcessor
            //{
            //    OnOrderInitialized = chain2,
            //    Complete = SendConfirmationEmail
            //};

            /// using Action and Func
            Func<Order, bool> chain2 = (order) =>
            {
                SendMessageToWareHouse(order);
                SendMessageToManager(order);
                SendMessageToBoss(order);

                return true;
            };
            var process = new OrderProcessor
            {
                OrderInitialized = chain2,
                Complete = SendConfirmationEmail
            };

            process.OrderCreated += (sender, args) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1");
            };

            process.OrderCreated += (sender, args) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("2");
            };

            process.OrderCreated += (sender, args) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("3");
            };

            #region Assign to event
            process.OrderCreatedInherit += (sender, args) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1");
                Console.WriteLine($"args include: {args?.Order?.GetType().Name} - NewToltal {args?.NewTotal} - OldTolal {args?.OldTotal}");
            };

            process.OrderCreatedInherit += (sender, args) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("2");
                Console.WriteLine($"args include: {args?.Order.GetType().Name} - NewToltal {args?.NewTotal}  - OldTolal  {args?.OldTotal}");
            };

            process.OrderCreatedInherit += (sender, args) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("3");
                Console.WriteLine($"args include: {args?.Order.GetType().Name} - NewToltal {args?.NewTotal}  - OldTolal  {args?.OldTotal}");
            };

            
            #endregion

            process.Process(oder, SendConfirmationEmail);
        }

        public static bool SendMessageToBoss(Order order)
        {
            Console.WriteLine("Send Message To Boss");
            return true;
        }

        public static bool SendMessageToManager(Order order)
        {
            Console.WriteLine("Send Message To Manager");
            return true;
        }
    }
}