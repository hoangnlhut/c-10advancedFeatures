using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Extentions
{
    public static class OrderExtentions
    {
        //extentions method 1
        public static string GenerateReport(this Order order)
        {
            return $"ORDER REPORT ({order.OrderNumber})" + 
                   $"{Environment.NewLine}" +
                   $"Items: {order.LineItems.Count()}" +
                   $"{Environment.NewLine}" +
                   $"Total: {order.Total}" +
                   $"{Environment.NewLine}" 
                   ;
        }

        // extentions method 2
        public static string GenerateReport(this Order order, string recipient)
        {
            return $"ORDER REPORT ({order.OrderNumber})" +
                   $"{Environment.NewLine}" +
                   $"Items: {order.LineItems.Count()}" +
                   $"{Environment.NewLine}" +
                   $"Total: {order.Total}" +
                   $"{Environment.NewLine}" +
                   $"Recipient: {recipient}"
                   ;
        }
    }
}
