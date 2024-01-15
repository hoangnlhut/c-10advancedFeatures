using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain
{
    public record Customer(string FirstName, string LastName);

    public record OtherPeople(string FirstName, string LastName) : Customer(FirstName, LastName);

    public record CustomerWithOrder(string FirstName, string LastName, IList<Order> Orders);
}
