using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain
{
    public record OrderRecord(ShippingProvider? ShippingProvider,
        IEnumerable<Item?>? LineItems,
        decimal Total, bool IsReadyForShipment = false
    )
    {
        public Guid OrderNumber { get; init; } = Guid.NewGuid();
    }
}
