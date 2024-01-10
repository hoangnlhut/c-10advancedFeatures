using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Cms;
using System.Net.Mail;
using System.Text.Json;
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
using WarehouseManagementSystem.Domain.Extentions;
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
                SendMessageToManager();
                SendMessageToBoss();

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

            //process.Process(oder, SendConfirmationEmail);

            //using extention method
            //foreach (var item in oder.LineItems.Find(item => item.Price > 600))
            //{
            //    Console.WriteLine(item?.Price);
            //}

            // Test Deconstruction Dictionary
            DeconstructionWithDictionary();

            //Test for Anonymous Type
            //AnonymousType();

            //Test Deconstruction Tuple
            DeconstructionTuple();

            //Test for Tuple
            UsingTuple();

            //call extentions method 1 for order
            var generateReport = oder.GenerateReport();
            Console.WriteLine($"{generateReport}");

            //call  method  from order class
            var generateReport2 = oder.GenerateReport("Recipient from Order Class");
            Console.WriteLine($"{generateReport2}");

            //call extentions method 2 for order --> it does not work because it will prioritize method from Order class first. --> we need to include clearly named parameter as in extention method 2
            var generateReport3 = oder.GenerateReport(recipient: "Recipient from Extention method 2");
            Console.WriteLine($"{generateReport3}");

        }

        public static bool SendMessageToBoss()
        {
            Console.WriteLine("Send Message To Boss");
            return true;
        }

        public static bool SendMessageToManager()
        {
            Console.WriteLine("Send Message To Manager");
            return true;
        }

        public static void AnonymousType()
        {
            var anonymousType = new { Hoang = "Hoang", Age = 40, Order = new Order() { OrderNumber = new Guid() } };
            Console.WriteLine($"Name : {anonymousType.Hoang} and Age : {anonymousType.Age}");

            //Modifying and Returning Anonymous Types
            var newInstanceOfAnonymousType = anonymousType with { Age = 1, Hoang = "Trang" };
            Console.WriteLine($"Name : {anonymousType.Hoang} and Age : {anonymousType.Age}");
            Console.WriteLine($"Name : {newInstanceOfAnonymousType.Hoang} and Age : {newInstanceOfAnonymousType.Age}");

            newInstanceOfAnonymousType.Order.IsReadyForShipment = false;
            Console.WriteLine($"IsReadyForShipment of newInstanceOfAnonymousType: {newInstanceOfAnonymousType.Order.IsReadyForShipment}");
            Console.WriteLine($"IsReadyForShipment of anonymousType: {anonymousType.Order.IsReadyForShipment}");
        }

        public static void DeconstructionTuple()
        {
            var order = new Order() { 
                LineItems = new List<Item>()
                {
                    new Item() {},
                    new Item() {},
                }
            };

            var (total, isReady) = order;
            if (order is (total: >0, true))
            {
                Console.WriteLine($"{order}");
            }
        }

        public static void DeconstructionWithDictionary()
        {
            Dictionary<string, Order> dict = new Dictionary<string, Order>()
            {
                {"Hoang", new Order() },
                {"Trang", new Order() }
            };

            foreach (var ( orderNumber, theOrder) in dict)
            {
                Console.WriteLine($"Order Number: {orderNumber} and Order : {theOrder}");
            }
        }

        public static void UsingTuple()
        {
            var order = new Order();

            var group = (order.OrderNumber, order.LineItems, Sum: order.LineItems == null ? 0 : order.LineItems.Sum(item => item.Price)); 

            // cách gán local variable vao tuple và cách giải mã
            // 1
            (var orderNumber, var items, var sum) = (order.OrderNumber, order.LineItems, Sum: order.LineItems == null ? 0 : order.LineItems.Sum(item => item.Price));

            // 2
            var ( orderNumber1,  items1,  sum1) = (order.OrderNumber, order.LineItems, Sum: order.LineItems == null ? 0 : order.LineItems.Sum(item => item.Price));

            // 3
            Guid orderNumber2;
            decimal sum2;
            (orderNumber2, var single2, sum2) = (order.OrderNumber, order.LineItems, Sum: order.LineItems == null ? 0 : order.LineItems.Sum(item => item.Price));


            // don't care about the value of item using _ character
            (var orderNumber3, _, var sum3) = (order.OrderNumber, order.LineItems, Sum: order.LineItems == null ? 0 : order.LineItems.Sum(item => item.Price));



            var json = JsonSerializer.Serialize(group);
            Console.WriteLine($"{json}");

            var json2= JsonSerializer.Serialize(group, options: new() { IncludeFields = true});
            Console.WriteLine($"{json2}");
        }
    }
}