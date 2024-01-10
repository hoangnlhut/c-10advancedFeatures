namespace WarehouseManagementSystem.Domain
{
    public class Order : IEquatable<Order?>
    {
        public Guid OrderNumber { get; init; }
        public ShippingProvider ShippingProvider { get; init; }
        public decimal Total { get; }
        public bool IsReadyForShipment { get; set; } = true;
        public IEnumerable<Item> LineItems { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Order);
        }

        public bool Equals(Order? other)
        {
            return other is not null &&
                   OrderNumber.Equals(other.OrderNumber) &&
                   Total == other.Total &&
                   IsReadyForShipment == other.IsReadyForShipment &&
                   EqualityComparer<IEnumerable<Item>>.Default.Equals(LineItems, other.LineItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderNumber, Total, IsReadyForShipment, LineItems);
        }

        public static bool operator ==(Order? left, Order? right)
        {
            return EqualityComparer<Order>.Default.Equals(left, right);
        }

        public static bool operator !=(Order? left, Order? right)
        {
            return !(left == right);
        }
        public  string GenerateReport(string re)
        {
            return $"ORDER REPORT ({OrderNumber})" +
                   $"{Environment.NewLine}" +
                   $"Items: {LineItems.Count()}" +
                   $"{Environment.NewLine}" +
                   $"Total: {Total}" +
                   $"{Environment.NewLine}" +
                   $"Recipient: {re}"
                   ;
        }

        //public void Deconstruct(out int total, out bool isReady)
        //{
        //    total = LineItems.Count();
        //    isReady = IsReadyForShipment;
        //}

        public void Deconstruct(out decimal total,
           out bool ready)
        {
            total = Total;
            ready = IsReadyForShipment;
        }

        public void Deconstruct(out decimal total,
            out bool ready,
            out IEnumerable<Item> items)
        {
            total = Total;
            ready = IsReadyForShipment;
            items = LineItems;
        }
    }

    public class PriorityOrder : Order { }

    public class ShippedOrder : Order
    {
        public DateTime ShippedDate { get; set; }
    }

    public class CancelledOrder : Order
    {
        public DateTime CancelledDate { get; set; }
    }


    public class ProcessedOrder : Order { }

}