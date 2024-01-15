using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessorRecord
    {
        public string GenerateReportFor(OrderRecord? order)
        {
            ArgumentNullException.ThrowIfNull(order);
            var provider = order?.ShippingProvider ?? new ShippingProvider();

            var providerName = provider?.Name ?? "No Provider"; ;

            //var report = $"{order?.OrderNumber}" +
            //    $"Items: {order?.LineItems?.Count() ?? 0}" +
            //    $"Provider: {providerName}";

            return providerName;
        }
    }
}
