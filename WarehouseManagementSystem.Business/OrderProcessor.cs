using WarehouseManagementSystem.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        //public delegate bool OrderInitialized(Order order);
        //public delegate void OnComplelte(Order order);

        /// <summary>
        ///  refactor for using Action<T> and Func<T, TResult>
        /// </summary>
        /// 
        public Func<Order, bool> OrderInitialized { get; set; }
        public Action<Order> Complete;

        /// <summary>
        /// declare event
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="Exception"></exception>
        /// 
        public event EventHandler OrderCreated;

        protected virtual void OnOrderCreated()
        {
            OrderCreated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// using event hanlder generic class which inherits EventArgs 
        /// </summary>
        /// <param name="order"></param>
        /// <exception cref="Exception"></exception>
        /// 
        public event EventHandler<OrderCreatedEventArgs> OrderCreatedInherit;

        protected virtual void OnOrderCreated(OrderCreatedEventArgs args)
        {
            OrderCreatedInherit?.Invoke(this, args);
        }

        private void Initialize(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);
           if(!OrderInitialized.Invoke(order))
            {
                throw new Exception("Could not Initialized..");
            }
            Complete.Invoke(order);
        }

        public void Process(Order order, Action<Order> onComplelte = default)
        {
            Initialize(order);
            OnOrderCreated();
            OnOrderCreated(new OrderCreatedEventArgs()
            { 
                Order = order,
                NewTotal = 80,
                OldTotal = 100
            });
            onComplelte?.Invoke(order);
        }

        public string GetOrderStatus(Order order)
        {
            var status = order switch
            {
                CancelledOrder or ShippedOrder => "Already handled",
                { Total: > 500m } => "High priority order",
                PriorityOrder or { Total: > 100m and < 500m } => "",
                not null and var instance => instance.OrderNumber.ToString()
            };
            return status;
        }
        public string GetShippingStatus(Order order)
        {
            var shippingProviderStatus = order switch
            {
                ( >= 10, true, _) => "Multiple shipments!",
                ( <= 3, _, SwedishPostalServiceShippingProvider) => "Manual pickup required",
                (_, true, _) => "Ready for shipment",
                _ => "Not ready for shipment"
            };
            return shippingProviderStatus;
        }

        public string GenerateReport(Order order)
        {
            return GetOrderStatus(order) +
                Environment.NewLine +
                GetShippingStatus(order);
        }
    }

    //public class BatchOrderProcessor : OrderProcessor
    //{
    //    protected override void OnOrderCreated()
    //    {
    //        base.OnOrderCreated();
    //    }
    //}

}
